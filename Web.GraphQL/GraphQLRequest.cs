using GraphQL.SystemTextJson;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Web.GraphQL
{
    public class GraphQLRequest
    {
        public string Query { get; set; }

        [JsonConverter(typeof(ObjectDictionaryConverter))]
        public Dictionary<string, object> Variables
        {
            get; set;
        }
    }
}