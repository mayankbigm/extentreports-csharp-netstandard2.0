using AventStack.ExtentReports.Model;

using MongoDB.Bson;
using System;

namespace AventStack.ExtentReports.MediaStorage
{
    internal class KlovMediaStorageHandler
    {
        public KlovMediaStorageHandler(string url, KlovMedia media)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("Invalid URL or resource not found");
            }

            _klovMedia = media;
            _mediaManager = new KlovHttpMediaManager();
            _mediaManager.Init(url);
        }

        public void SaveScreenCapture(IBasicMongoReportElement element, ScreenCapture media)
        {


            var document = new BsonDocument
            {
                { "project", _klovMedia.ProjectId },
                { "report", _klovMedia.ReportId },
                { "sequence", media.Sequence },
                { "mediaType", "img" },
                { "test", media.TestObjectId }
            };

            Test test = (Test) element;
            if (test != null)
            {
                document.Add("testName", test.Name);
            }
            else
            {
                document.Add("log", element?.ObjectId);
            }

            _klovMedia.MediaCollection.InsertOne(document);
            var id = document["_id"].AsObjectId;
            media.ObjectId = id;
            media.ReportObjectId = _klovMedia.ReportId;
            _mediaManager.StoreMedia(media);
        }

        private readonly KlovHttpMediaManager _mediaManager;
        private readonly KlovMedia _klovMedia;
    }
}
