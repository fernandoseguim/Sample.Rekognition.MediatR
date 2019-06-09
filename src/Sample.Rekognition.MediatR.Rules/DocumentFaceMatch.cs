using System.Collections.Generic;
using System.Linq;
using Amazon.Rekognition.Model;

namespace Sample.Rekognition.MediatR.Rules
{
    public class DocumentFaceMatch
    {
        public DocumentFaceMatch(ICollection<CompareFacesMatch> labels) { Labels = labels; }

        public ICollection<CompareFacesMatch> Labels { get; }

        public bool IsSameFaceAsync(float threshold)
        {
            var hasOneResult = HasOneResult(Labels);
            var isSame = Labels.First().Similarity >= threshold;

            return hasOneResult && isSame;
        }

        private static bool HasOneResult(ICollection<CompareFacesMatch> labels) => labels.Count == 1;
    }
}
