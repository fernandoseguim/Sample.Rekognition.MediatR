using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace Sample.Rekognition.MediatR.Rules.Requests
{
    public class DetectLabelsService : IDetectLabelsService
    {
        public DetectLabelsService(AmazonRekognitionClient client)
        {
            Client = client;
            Bucket = "poc-s3-rekognition";
        }

        public AmazonRekognitionClient Client { get; }
        public string Bucket { get; }

        public async Task<ICollection<Label>> AnalisyAsync(string image)
        {
            var response = await Client.DetectLabelsAsync(new Amazon.Rekognition.Model.DetectLabelsRequest()
            {
                Image = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new S3Object()
                    {
                        Bucket = Bucket,
                        Name = image,
                    }
                }
            });

            return response.Labels;
        }
    }

    public interface IDetectLabelsService
    {
        Task<ICollection<Label>> AnalisyAsync(string image);
    }
}
