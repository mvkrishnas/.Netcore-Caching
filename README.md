## Caching in .Net Core In memory and Distributed redis cache 

About Caching.

	•	Caching refers to the process of storing frequently used data so that those data can be served much faster for any future requests. So we take the most frequently used data and copy it into temporary storage so that it can be accessed much faster in future calls from the client.

Add NuGet packages

	•	microsoft.extensions.caching.stackexchangeredis

Add extension in Configureservices in startup.cs file

	•	services.AddStackExchangeRedisCache(option => option.Configuration ="localhost:6379");
 
	•	services.AddMemoryCache();
