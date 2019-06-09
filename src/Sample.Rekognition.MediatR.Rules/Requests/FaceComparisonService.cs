using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace Sample.Rekognition.MediatR.Rules.Requests
{
    public class FaceComparisonService : IFaceComparisonService
    {
        public FaceComparisonService(AmazonRekognitionClient client)
        {
            Client = client;
            Bucket = "poc-s3-rekognition";
        }

        public AmazonRekognitionClient Client { get; }
        public string Bucket { get; }

        public async Task<ICollection<CompareFacesMatch>> CompareAsync(float threshold, string source, string target)
        {
            var response = await Client.CompareFacesAsync(new CompareFacesRequest()
            {
                SimilarityThreshold = threshold,
                SourceImage = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new S3Object()
                    {
                        Bucket = Bucket,
                        Name = source
                    }
                },

                TargetImage = new Amazon.Rekognition.Model.Image()
                {
                    S3Object = new S3Object()
                    {
                        Bucket = Bucket,
                        Name = target
                    }
                }
            });

            return response.FaceMatches;
        }
    }

    public interface IFaceComparisonService
    {
        Task<ICollection<CompareFacesMatch>> CompareAsync(float threshold, string source, string target);
    }
}
