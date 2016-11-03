using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ND.Component.Extentions;
using System.Threading;

//**********************************************************************
//
// 文件名称(File Name)：Run.CS        
// 功能描述(Description)：     
// 作者(Author)：Aministrator               
// 日期(Create Date)： 2016/10/20 15:24:44         
//
// 修改记录(Revision History)： 
//       R1:
//             修改作者:          
//             修改日期:2016/10/20 15:24:44          
//             修改理由：         
//**********************************************************************
namespace ND.Component.Utility
{
    public static class Run
    {
       

        public static Task InParallel(int iterations, Func<int, Task> work) {
            return Task.WhenAll(Enumerable.Range(1, iterations).Select(i => Task.Run(() => work(i))));
        }

        public static async Task WithRetriesAsync(Func<Task> action, int maxAttempts = 5, TimeSpan? retryInterval = null, CancellationToken cancellationToken = default(CancellationToken)) {
            await WithRetriesAsync(async () => {
                await action().AnyContext();
                return Task.FromResult(false);
            }, maxAttempts, retryInterval, cancellationToken).AnyContext();
        }

        public static async Task<T> WithRetriesAsync<T>(Func<Task<T>> action, int maxAttempts = 5, TimeSpan? retryInterval = null, CancellationToken cancellationToken = default(CancellationToken)) {
            if (action == null)
                throw new ArgumentNullException(action.ToString());

            int attempts = 1;
            var startTime = DateTime.UtcNow;
            do {
                if (attempts > 1)
                   

                try {
                    return await action().AnyContext();
                } catch (Exception ex) {
                    if (attempts >= maxAttempts)
                        throw;

                    Thread.Sleep(retryInterval ?? TimeSpan.FromMilliseconds(attempts * 100));
                }

                attempts++;
            } while (attempts <= maxAttempts && !cancellationToken.IsCancellationRequested);

            throw new TaskCanceledException("Should not get here.");
        }
    }
}
