#region More advanced schema and queries
//  var schema = Schema.For(@"
//        type Query {
//            game: Playable
//        }

//        type Playable {
//            title: String
//            price: Decimal
//            status: String
//        }",
//      _ =>
//      {
//          _.Types.Include<GameType>();
//          _.Types.Include<Query>();
//      }
//  );

//  var json = await schema.ExecuteAsync(_ =>
//  {
//      _.Query = "{ game { title price status } }";
//  });

//  Console.WriteLine(json);

//  class Query
//  {
//      [GraphQLMetadata("game")]
//      public Playable GetPlayable()
//      {
//          return new Playable
//          {
//              Title = "Grand Theft Auto V",
//              Price = 9.99M,
//              IsOnSale = false
//          };
//      }
//  }

//  class Playable
//  {
//      public string Title { get; set; }
//      public decimal Price { get; set; }
//      public bool IsOnSale { get; set; }
//  }

//  [GraphQLMetadata("Playable", IsTypeOf = typeof(Playable))]
//  class GameType
//  {
//      public string Title(Playable playable) => playable.Title;
//      public decimal Price(Playable playable) => playable.Price;
//      public string Status(Playable playable) => playable.IsOnSale ? "On Sale" : "Regular Price";
//  }
#endregion