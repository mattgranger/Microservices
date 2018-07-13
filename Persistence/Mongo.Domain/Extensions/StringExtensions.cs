namespace System
{
    using MongoDB.Bson;

    public static class StringExtensions
    {
        public static string ToObjectId(this string input)
        {
            return ObjectId.GenerateNewId().ToString();
        }
    }
}
