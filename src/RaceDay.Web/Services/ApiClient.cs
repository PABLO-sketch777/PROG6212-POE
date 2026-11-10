using System.Net.Http.Json; using System.Text.Json; using RaceDay.Web.Models;
namespace RaceDay.Web.Services;
public class ApiClient(HttpClient http,IHttpContextAccessor accessor){
 static readonly JsonSerializerOptions Json=new(JsonSerializerDefaults.Web); HttpContext C=>accessor.HttpContext!;
 void Cookie(){if(C.Session.GetString("ApiCookie") is string c)http.DefaultRequestHeaders.Remove("Cookie");if(C.Session.GetString("ApiCookie") is string v)http.DefaultRequestHeaders.Add("Cookie",v);}
 async Task<T?> Send<T>(HttpRequestMessage req){Cookie();using var res=await http.SendAsync(req);if(res.Headers.TryGetValues("Set-Cookie",out var cs))C.Session.SetString("ApiCookie",cs.First().Split(';')[0]);if(!res.IsSuccessStatusCode)return default;return await res.Content.ReadFromJsonAsync<T>(Json);}
 public async Task<UserModel?> Login(LoginModel m)=>await Send<UserModel>(new(HttpMethod.Post,"api/auth/login"){Content=JsonContent.Create(m)});
 public async Task<UserModel?> Register(RegisterModel m)=>await Send<UserModel>(new(HttpMethod.Post,"api/auth/register"){Content=JsonContent.Create(m)});
 public async Task Logout(){Cookie();await http.PostAsync("api/auth/logout",null);C.Session.Clear();}
 public async Task<List<EventModel>> Events()=>await Send<List<EventModel>>(new(HttpMethod.Get,"api/events"))??[];
 public async Task<EventModel?> Event(int id)=>await Send<EventModel>(new(HttpMethod.Get,$"api/events/{id}"));
 public async Task<List<CategoryModel>> Categories(int id)=>await Send<List<CategoryModel>>(new(HttpMethod.Get,$"api/events/{id}/categories"))??[];
 public async Task<HttpResponseMessage> Raw(HttpMethod method,string path,HttpContent? content=null){Cookie();var r=await http.SendAsync(new(method,path){Content=content});return r;}
 public async Task<T?> Get<T>(string path)=>await Send<T>(new(HttpMethod.Get,path));
 public async Task<T?> Post<T>(string path,object body)=>await Send<T>(new(HttpMethod.Post,path){Content=JsonContent.Create(body)});
 public async Task<bool> Delete(string path){Cookie();var r=await http.DeleteAsync(path);return r.IsSuccessStatusCode;}
 public void SetUser(UserModel u)=>C.Session.SetString("User",JsonSerializer.Serialize(u,Json)); public UserModel? User=>C.Session.GetString("User") is string s?JsonSerializer.Deserialize<UserModel>(s,Json):null;
}
