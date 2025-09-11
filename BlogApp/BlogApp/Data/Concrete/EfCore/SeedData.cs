using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore
{
    public static class SeedData
    {
        public static void FillTestDatas(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();

            if(context != null)
            {
                if(context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                if(!context.Tags.Any())
                {
                    context.Tags.AddRange(
                        new Tag { Text = "web programlama", Url = "web-programlama", Color = TagColors.warning },
                        new Tag { Text = "backend", Url="backend", Color = TagColors.info },
                        new Tag { Text = "frontend", Url="frontend" , Color = TagColors.success },
                        new Tag { Text = "fullstack", Url="fullstack", Color = TagColors.secondary  },
                        new Tag { Text = "php", Url="php", Color = TagColors.primary  }
                    );
                    context.SaveChanges();
                }

                if(!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { UserName = "sadikturan", Image = "p1.jpg"},
                        new User { UserName = "ahmetyilmaz", Image = "p2.jpg"}
                    );
                    context.SaveChanges();
                }

                if(!context.Posts.Any())
                {
                    context.Posts.AddRange(
                        new Post {
                            Title = "Asp.net core",
                            Content = "ASP.NET Core ile modern, ölçeklenebilir web uygulamaları geliştiriyoruz. Middleware, dependency injection, routing, controller ve action yapıları anlaşılır örneklerle anlatılıyor. Entity Framework Core ile veri erişimi, migration yönetimi ve repository deseni uygulanıyor. Konfigürasyon, logging, hata işleme ve çevresel ayarlar tanıtılıyor. Minimal API ile MVC karşılaştırılıyor. Kimlik doğrulama, yetkilendirme, test stratejileri ve yayınlama senaryoları pratik ipuçlarıyla tamamlanıyor. Örnek kodlar ve yaklaşımlar sunuluyor.",
                            Url = "aspnet-core",
                            IsActive = true,
                            PublishedOn = DateTime.Now.AddDays(-10),
                            Tags = context.Tags.Take(3).ToList(),
                            Image = "asp.png",
                            UserId = 1,
                            Comments = new List<Comment> { 
                                new Comment { Text = "Çok detaylı açıklamışsınız, tebrikler!", PublishedOn = new DateTime(), UserId = 1},
                                new Comment { Text = "Elinize sağlık, başarılar.", PublishedOn = new DateTime(), UserId = 2},
                            }
                        },
                        new Post {
                            Title = "Php",
                            Content = "PHP ile dinamik web geliştirmeye sağlam bir başlangıç yapıyoruz. Söz dizimi, fonksiyonlar, diziler, nesne yönelimli programlama ve hata ayıklama adım adım inceleniyor. PDO ile güvenli veritabanı işlemleri, hazırlıklı ifadeler ve transaction yönetimi uygulanıyor. Composer, PSR standartları ve autoloading tanıtılıyor. Form doğrulama, oturum yönetimi, RESTful servisler, güvenlik ipuçları ve dağıtım stratejileri pratik bir blog projesiyle pekiştiriliyor. gerçek dünya örnekleri eşliğinde anlatılıyor.",
                            Url = "php",
                            IsActive = true,
                            Image = "php.png",
                            PublishedOn = DateTime.Now.AddDays(-20),
                            Tags = context.Tags.Take(2).ToList(),
                            UserId = 1
                        },
                        new Post {
                            Title = "Django",
                            Content = "Django ile hızlı, güvenli ve bakımı kolay web uygulamaları geliştiriyoruz. Proje yapısı, uygulama kavramı, URL yönlendirme ve view katmanı net biçimde açıklanıyor. MTV akışı, ORM sorguları, migration süreçleri ve admin paneli özelleştirme ele alınıyor. Formlar, doğrulama, kimlik doğrulama ve yetkilendirme işleniyor. Class-based ve generic views yaklaşımları, test yazımı, ortam ayarları ve dağıtım stratejileri uygulanıyor. örnek kodlarla en iyi kalıplar paylaşılıyor.",
                            Url = "django",
                            IsActive = true,
                            Image = "django.png",
                            PublishedOn = DateTime.Now.AddDays(-30),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "React Dersleri",
                            Content = "React ile bileşen tabanlı arayüz geliştirmeyi temelden ileri seviyeye taşıyoruz. JSX, props, state, lifecycle mantığı ve fonksiyon bileşenleri açıklanıyor. Hooks, context ve custom hook tasarımıyla tekrar kullanılabilirlik sağlanıyor. Formlar, doğrulama, veri çekme, loading durumları ve hata sınırları işleniyor. performans için memoization, useMemo, useCallback ve kod bölme öğretiliyor. Router, test stratejileri ve erişilebilirlik en iyi uygulamalarla anlatılıyor. gerçek proje örnekleriyle pekiştiriliyor.",
                            Url = "react-dersleri",
                            IsActive = true,
                            Image = "react.png",
                            PublishedOn = DateTime.Now.AddDays(-40),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "Angular",
                            Content = "Angular ile güçlü, tip güvenli tek sayfa uygulamaları geliştiriyoruz. Modüller, bileşenler, şablon sözdizimi, veri bağlama ve pipe kullanımı anlatılıyor. Dependency injection, servisler, RxJS ve observable tabanlı akışlar uygulanıyor. Router, guard ve resolver ile gezinme tasarlanıyor. Reactive Forms, doğrulama ve dinamik formlar işleniyor. HttpClient, interceptor, hata yönetimi ve performans odaklı değişiklik algılama stratejileri; test, erişilebilirlik ve dağıtım pratikleriyle tamamlanıyor. örneklerle anlatılıyor.",
                            Url = "angular",
                            IsActive = true,
                            Image = "angular.png",
                            PublishedOn = DateTime.Now.AddDays(-50),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }
                        ,
                        new Post {
                            Title = "Web Tasarım",
                            Content = "Web tasarım ilkeleriyle kullanıcı odaklı, erişilebilir ve hızlı arayüzler tasarlıyoruz. Tipografi, renk kuramı, kontrast, boşluk kullanımı ve görsel hiyerarşi konu ediliyor. Responsive grid sistemleri, mobil öncelikli yaklaşım ve modern CSS teknikleri uygulanıyor. Bileşen tabanlı tasarım, yeniden kullanılabilirlik ve tasarım sistemleri kuruluyor. Performans optimizasyonu, görsel sıkıştırma, lazy loading ve Core Web Vitals ölçümü; kullanılabilirlik testleri ve mikro etkileşimlerle pekiştiriliyor. örneklerle anlatılıyor.",
                            Url = "web-tasarim",
                            IsActive = true,
                            Image = "web.jpeg",
                            PublishedOn = DateTime.Now.AddDays(-60),
                            Tags = context.Tags.Take(4).ToList(),
                            UserId = 2
                        }
                    );
                    context.SaveChanges();
                }
            }
        }
    }
}