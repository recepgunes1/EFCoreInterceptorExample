# EFCoreInterceptorExample

Bu proje, .NET üzerinde Entity Framework Core ile Interceptor kullanımına örnektir. Aşağıdaki adımları takip ederek projeyi kurabilir ve test edebilirsiniz.

## Kurulum
1. Repoyu klonlayın:
   ```sh
   git clone https://github.com/recepgunes1/EFCoreInterceptorExample.git
   cd EFCoreInterceptorExample
   ```

2. Uygulamayı ayağa kaldırın yükleyin:
   ```sh
   docker-compose up -d
   ```
  
3. `EFCoreInterceptorExample.http` dosyasında aşağıdaki istek ile veritabanına gerekli migration'lar kurulur:
   ```
   GET {{EFCoreInterceptorExample_HostAddress}}/init_db
   Accept: application/json
   ```

4. `EFCoreInterceptorExample.http` dosyasında yer alan diğer endpointler ile çeşitli denemeler yapabilirsiniz. 

---

Bu talimatları takip ederek `EFCoreInterceptorExample` projesini kurabilir, kullanabilir ve test edebilirsiniz. İyi çalışmalar!
