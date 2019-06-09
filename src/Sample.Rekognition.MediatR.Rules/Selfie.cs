using System.Collections.Generic;
using System.Linq;
using Amazon.Rekognition.Model;

namespace Sample.Rekognition.MediatR.Rules
{
    public class Selfie : Image
    {
        private static readonly string[] InvalidLabels = { "Photo", "Photography", "Portrait" };

        private static readonly string[] ValidPersonLabels = { "Human", "Person", "Face", "Head", "Selfie", "People" };

        private static readonly string[] ValidDocumentLabels = { "Document", "Id Cards", "License", "Driving License" };

        public Selfie(ICollection<Label> labels) : base(labels) { }

        public bool IsValidSelfie(float confidence) => HavePersonLabel(confidence) && HaveDocumentLabel(confidence) && NotHaveInvalidLabel();

        private bool HavePersonLabel(float confidence) => Labels
                                                        .Where(label => label.Confidence > confidence)
                                                        .Any(label => ValidPersonLabels.Any(validLabel => validLabel.Equals(label.Name)));
        private bool HaveDocumentLabel(float confidence) => Labels
                                                          .Where(label => label.Confidence > confidence)
                                                          .Any(label => ValidDocumentLabels.Any(validLabel => validLabel.Equals(label.Name)));
        private bool NotHaveInvalidLabel() => Labels.Any(label => InvalidLabels.Any(validLabel => validLabel.Equals(label.Name))) == false;
    }
}