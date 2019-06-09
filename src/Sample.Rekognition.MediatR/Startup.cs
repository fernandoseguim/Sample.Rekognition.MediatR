using Amazon;
using Amazon.Rekognition;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Rekognition.MediatR.Pipeline;
using Sample.Rekognition.MediatR.Rules.Requests;

namespace Sample.Rekognition.MediatR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            const string ID = "AKIAVSTVILGIQYHQQHPU";
            const string KEY = "U9+As7vI7I5h44qqOonF3mccLmoabSD3mNHMi7/m";

            services.AddScoped(client => new AmazonRekognitionClient(ID, KEY, RegionEndpoint.USEast1));

            services.AddScoped<IDetectLabelsService, DetectLabelsService>();
            services.AddScoped<IFaceComparisonService, FaceComparisonService>();

            services.AddScoped<IPipelineBehavior<Message, Result>, SelfieHandler>();
            services.AddScoped<IPipelineBehavior<Message, Result>, DocumentFrontHandler>();
            services.AddMediatR(typeof(DocumentFaceMatchHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseMvc();
        }
    }
}
