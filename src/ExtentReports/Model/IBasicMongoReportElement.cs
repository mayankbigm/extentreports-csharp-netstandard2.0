using MongoDB.Bson;

namespace AventStack.ExtentReports.Model
{
    interface IBasicMongoReportElement
    {
        ObjectId ObjectId { get; set; }
    }
}
