using Polly_Demo.src;

/*ExceptionPolly exceptionPolly = new ExceptionPolly();
exceptionPolly.Execute();
exceptionPolly.CommExecute();*/


/*CircuitBreakerPolly circuitBreaker = new CircuitBreakerPolly();
circuitBreaker.RunCircuitBreaker();*/


/*TimeOutPolly timeOutPolly = new TimeOutPolly();
timeOutPolly.RunTimeoutPolicy();*/


/*CachePolly cachePolly = new CachePolly();
cachePolly.RunCachePolly();*/


BulkheadPolly bulkheadPolly = new BulkheadPolly();
await bulkheadPolly.RunBulkheadPolly();