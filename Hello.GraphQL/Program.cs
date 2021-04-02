﻿using GraphQL;
using GraphQL.NewtonsoftJson;
using GraphQL.Types;
using System;
using System.Threading.Tasks;

namespace Hello.GraphQL
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            /* Schema first approach */
            var schemaFirst = Schema.For(@"
                type Query {
                    hello: String
                }
            ");

            /* Code first approach */
            var codeFirst = new Schema { Query = new HelloWorldQuery() };

            var schemaFirstJson = await schemaFirst.ExecuteAsync(_ =>
            {
                _.Query = "{ hello }";
                _.Root = new { Hello = "world" };
            });

            var codeFirstJson = await codeFirst.ExecuteAsync(_ =>
            {
                _.Query = "{ hello }";
            });

            Console.WriteLine("\nSchema First Approach\n");
            Console.WriteLine(schemaFirstJson);
            Console.WriteLine("\nCode First Approach\n");
            Console.WriteLine(codeFirstJson);
        }
    }

    internal class HelloWorldQuery : ObjectGraphType
    {
        public HelloWorldQuery()
        {
            Field<StringGraphType>(
                name: "hello",
                resolve: context => "world"
            );
        }
    }
}