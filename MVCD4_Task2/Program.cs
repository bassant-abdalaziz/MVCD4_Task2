var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}


app.Use(async (cont, nex) =>
{
    if (cont.Request.Cookies.ContainsKey("ReqNum"))
    {
        
        int num = int.Parse(cont.Request.Cookies["ReqNum"]);
        cont.Response.Cookies.Append("ReqNum", (++num).ToString());
        if(num == 10)
        {
            cont.Response.Cookies.Append("Rate", "1 Star"); 
        }

        else if (num == 20)
        {
            cont.Response.Cookies.Append("Rate", "2 Stars");
        }

        else if (num == 30)
        {
            cont.Response.Cookies.Append("Rate", "3 Stars");
        }

        else if (num == 40)
        {
            cont.Response.Cookies.Append("Rate", "4 Stars");
        }
        else if (num >= 50)
        {
            cont.Response.Cookies.Append("Rate", "5 Stars");
        }

    }
    else
    {
        cont.Response.Cookies.Append("ReqNum", "1");
    }
    await nex();
   
});


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
