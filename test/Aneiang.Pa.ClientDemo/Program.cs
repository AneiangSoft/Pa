using Aneiang.Pa.AspNetCore.Extensions;
using Aneiang.Pa.Lottery.Extensions;
using Aneiang.Pa.News.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 业务服务
builder.Services.AddNewsScraper(builder.Configuration);
builder.Services.AddLotteryScraper();

// 一站式注册（ScraperController + 缓存(内存/Redis)）
builder.Services.AddPaScraperApi(builder.Configuration).AddPaScraperAuthorization(builder.Configuration);

// 授权按需启用：配置文件 + 可选代码覆盖（示例：自定义策略）
//builder.Services.AddPaScraperAuthorization(builder.Configuration, configure: options =>
//{
//    // 示例：如果你想用配置文件里的 ApiKey，就删除这段即可。
//    // 这里演示自定义策略（可按需调整为 ApiKey/Combined）
//    options.Enabled = true;
//    options.Scheme = Aneiang.Pa.AspNetCore.Options.AuthorizationScheme.Custom;
//    options.CustomAuthorizationFunc = httpContext =>
//    {
//        var token = httpContext.Request.Headers["X-Demo-Token"].ToString();
//        return token == "valid-token" ? (true, null) : (false, null);
//    };
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
