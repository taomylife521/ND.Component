using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using ND.Component.Caching;
using ND.Component.LoadBalance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//**********************************************************************
//
// 文件名称(File Name)：MongoDBCache.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/11/8 15:53:40         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/11/8 15:53:40          
//             修改理由：         
//**********************************************************************
namespace ND.Component.MongoDB.Caching
{
   public class MongoDBCache:CacheBase
    {

        #region Set Value
        public override bool SetValue<T>(string key, T value, Cachelimit cacheLimit, DateTime? expireDate)
        {
            Server server = ChooseServer(key);
            MongoDBCacheEntity cache = new MongoDBCacheEntity();
            cache.ApplicationName = "";
            cache.Created = DateTime.Now;
            cache.CacheKey = key;
            cache.ExpireDate = cacheLimit == Cachelimit.Forever ? Convert.ToDateTime("2099-12-30") : (cacheLimit == Cachelimit.CurrentDay ? Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59") : Convert.ToDateTime(expireDate));
            cache.CacheSta = CacheStatus.Effective;


            cache.CacheValue = JsonConvert.SerializeObject(value);

            MongoCollection<MongoDBCacheEntity> collection = GetMongoDBCollection(server.ConnStr);
            if (collection != null)
            {
                try
                {
                    var query = Query.And(Query.EQ("CacheKey", key));
                    collection.Remove(query);
                }
                catch (Exception ex)
                { }
                var document = BsonSerializer.Deserialize<BsonDocument>(JsonConvert.SerializeObject(cache));
                WriteConcernResult res = collection.Save(document);
                TimeSpan timespan = cache.ExpireDate.Subtract(DateTime.Now);
                if (cacheLimit != Cachelimit.Forever)
                {
                    if (!collection.IndexExists(new IndexKeysBuilder().Ascending("ExpireDate")))
                    {
                        collection.EnsureIndex(new IndexKeysBuilder().Ascending("ExpireDate"), IndexOptions.SetTimeToLive(timespan));
                    }
                }
                OnOperating("写入完成end:key:" + key + ",serverConnstr:" + server.connstr + ",result:" + res.Ok + "," + res.ErrorMessage);
                return true;

            }
            return false;

        } 
        #endregion

        #region Get Value
        public override object GetValue(string key)
        {
           
            try
            {

                MongoDBCacheEntity entity = GetMongoDBEntity(key);
                if (entity != null)
                {
                    OnOperating("获取value成功:end-->:key:" + key +  ",value:" + entity.CacheValue);
                    return entity.CacheValue;
                }
                OnOperating("获取value失败:未找到key:" + key + "对应的值");
                return null;
            }
            catch (Exception ex)
            {
                OnOperating("获取value异常:" + JsonConvert.SerializeObject(ex) + "");
                return null;
            }
        }
        
        #endregion


        #region TryGetValue
        public override bool TryGetValue<T>(string key, out T value)
        {
            try
            {
                MongoDBCacheEntity entity = GetMongoDBEntity(key);
                value = default(T);
                value = JsonConvert.DeserializeObject<T>(entity.CacheValue);
                return true;
            }
            catch (Exception ex)
            {
                OnOperating("尝试获取值异常：exception:key:" + key + ",excption:" + JsonConvert.SerializeObject(ex));
                value = default(T);
                return false;
            }
        } 
        #endregion

        #region DeleteValue
        public override bool DeleteValue(string key)
        {
            try
            {
                Server server = ChooseServer(key);
                MongoCollection<MongoDBCacheEntity> collection = GetMongoDBCollection(server.ConnStr);

                var query2 = Query.And(
                                    Query.EQ("CacheKey", key)
                                    );
                WriteConcernResult res = collection.Remove(query2);//移除文档
                //清空过期的集合数据

                if (res.Ok)
                {
                    OnOperating("删除值完成:end:key:" + key + ",result:" + res.Ok + "," + res.ErrorMessage);
                    return true;

                }

                return false;
            }
            catch (Exception ex)
            {
                OnOperating("删除值异常:key:" + key + ",exception:" + JsonConvert.SerializeObject(ex));
                return false;
            }
        } 
        #endregion

        #region GetMongoDBCollection
        private MongoCollection<MongoDBCacheEntity> GetMongoDBCollection(string connStr)
        {


            MongoClient client = new MongoClient(connStr);
            MongoServer srv = client.GetServer();
            MongoDatabase db = srv.GetDatabase(CacheDBName);
            //string tableName = cacheLimit == Cachelimit.Forever ? CacheTableName + "Forever" : CacheTableName + "ByExpireDate";
            string tableName = CacheTableName;
            if (!db.CollectionExists(tableName))
                db.CreateCollection(tableName);

            MongoCollection<MongoDBCacheEntity> collection = db.GetCollection<MongoDBCacheEntity>(tableName);

            return collection;

        } 
        #endregion

        #region GetMongoDBEntity
        private MongoDBCacheEntity GetMongoDBEntity(string key)
        {
            try
            {
                Server server = ChooseServer(key);
                MongoCollection<MongoDBCacheEntity> collection = GetMongoDBCollection(server.ConnStr);
                var query2 = Query.And(
                                   Query.EQ("CacheKey", key)
                    //Query.GT("Expires", DateTime.Now),
                    // Query.EQ("CacheSta", "1")
                    //Query.EQ("ApplicationName", pApplicationName)
                                   );
                return collection.FindOne(query2);
            }
            catch (Exception ex)
            {
                OnOperating("GetMongoDBEntity异常,excption:" + JsonConvert.SerializeObject(ex));
                return null;
            }
        } 
        #endregion

        #region GetList
        public override List<CacheKeyMapDescriptor> GetList(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate)
        {
            List<CacheKeyMapDescriptor> lstCacheEntity = new List<CacheKeyMapDescriptor>();
            foreach (var item in serverConfig)
            {
                MongoCollection<MongoDBCacheEntity> collection = GetMongoDBCollection(item.ConnStr);
                List<MongoDBCacheEntity> lstEntity = collection.FindAll().ToList();
                if (dateType == DateType.CreateTime)
                {
                    switch (cacheExpire)
                    {
                        case CacheExpire.Expired://已过期 包括过期和无效
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Invalid && x.ExpireDate < DateTime.Now && x.Created >= startDate && x.Created < endDate).ToList();
                            break;
                        case CacheExpire.NoExpired://未过期
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= DateTime.Now && x.Created >= startDate && x.Created < endDate).ToList();
                            break;
                        default:
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.Created >= startDate && x.Created < endDate).ToList();
                            break;
                    }


                }
                else if (dateType == DateType.ExpireTime)
                {

                    switch (cacheExpire)
                    {
                        case CacheExpire.Expired://已过期 包括过期和无效
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Invalid && x.ExpireDate < DateTime.Now && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                            break;
                        case CacheExpire.NoExpired://未过期
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= DateTime.Now && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                            break;
                        default:
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                            break;
                    }
                }
                lstCacheEntity.AddRange(ChangeToCacheKeyMapDescriptor(lstEntity));

            }
            return lstCacheEntity;
        } 
        #endregion

        #region ChangeToCacheKeyMapDescriptor
        private List<CacheKeyMapDescriptor> ChangeToCacheKeyMapDescriptor(List<MongoDBCacheEntity> lstEntity)
        {
            List<CacheKeyMapDescriptor> lstCacheEntity = new List<CacheKeyMapDescriptor>();
            lstEntity.ForEach(x =>
            {
                lstCacheEntity.Add(new CacheKeyMapDescriptor()
                {
                    CacheKey = x.CacheKey,
                    CacheServerName = x.ApplicationName,
                    CacheSta = x.CacheSta,
                    CreateTime = x.Created,
                    ExpireDate = x.ExpireDate
                });
            });
            return lstCacheEntity;
        } 
        #endregion

        #region BulkDeleteValue
        public override bool BulkDeleteValue(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate)
        {
            List<string> lstCacheEntity = new List<string>();
            foreach (var item in serverConfig)
            {
                MongoCollection<MongoDBCacheEntity> collection = GetMongoDBCollection(item.ConnStr);
                List<MongoDBCacheEntity> lstEntity = collection.FindAll().ToList();
                if (dateType == DateType.CreateTime)
                {
                    switch (cacheExpire)
                    {
                        case CacheExpire.Expired://已过期 包括过期和无效
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Invalid && x.ExpireDate < DateTime.Now && x.Created >= startDate && x.Created < endDate).ToList();
                            break;
                        case CacheExpire.NoExpired://未过期
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= DateTime.Now && x.Created >= startDate && x.Created < endDate).ToList();
                            break;
                        default:
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.Created >= startDate && x.Created < endDate).ToList();
                            break;
                    }


                }
                else if (dateType == DateType.ExpireTime)
                {

                    switch (cacheExpire)
                    {
                        case CacheExpire.Expired://已过期 包括过期和无效
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Invalid && x.ExpireDate < DateTime.Now && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                            break;
                        case CacheExpire.NoExpired://未过期
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= DateTime.Now && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                            break;
                        default:
                            lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                            break;
                    }
                }
                lstEntity.ForEach(x =>
                {
                    lstCacheEntity.Add(x.CacheKey);
                });
                

            }
            bool flag = true;
            lstCacheEntity.ForEach(x =>
            {
               if(!DeleteValue(x))
               {
                   flag = false;
               }
            });
            return flag;
        } 
        #endregion

        #region GetAllKeys
        public override List<string> GetAllKeys(CacheExpire cacheExpire, DateType dateType, DateTime startDate, DateTime endDate)
        {
            List<string> lstKeys = new List<string>();
            foreach (var item in serverConfig)
            {
                 MongoCollection<MongoDBCacheEntity> collection = GetMongoDBCollection(item.ConnStr);
                 List<MongoDBCacheEntity> lstEntity = collection.FindAll().ToList();
                 if (dateType == DateType.CreateTime)
                 {
                     switch (cacheExpire)
                     {
                         case CacheExpire.Expired://已过期 包括过期和无效
                             lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Invalid && x.ExpireDate < DateTime.Now && x.Created >= startDate && x.Created < endDate).ToList();
                             break;
                         case CacheExpire.NoExpired://未过期
                             lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= DateTime.Now && x.Created >= startDate && x.Created < endDate).ToList();
                             break;
                         default:
                             lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.Created >= startDate && x.Created < endDate).ToList();
                             break;
                     }


                 }
                 else if (dateType == DateType.ExpireTime)
                 {

                     switch (cacheExpire)
                     {
                         case CacheExpire.Expired://已过期 包括过期和无效
                             lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Invalid && x.ExpireDate < DateTime.Now && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                             break;
                         case CacheExpire.NoExpired://未过期
                             lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= DateTime.Now && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                             break;
                         default:
                             lstEntity = lstEntity.Where(x => x.CacheSta == CacheStatus.Effective && x.ExpireDate >= startDate && x.ExpireDate < endDate).ToList();
                             break;
                     }
                 }
                 lstEntity.ForEach(x =>
                 {
                     lstKeys.Add(x.CacheKey);
                 });
            }
            return lstKeys;
        } 
        #endregion
    }
}
