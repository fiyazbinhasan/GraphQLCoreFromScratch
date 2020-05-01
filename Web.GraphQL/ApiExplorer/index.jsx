import React from "react";
import ReactDOM from "react-dom";
import GraphiQL from "graphiql";
import fetch from "isomorphic-fetch";
import "graphiql/graphiql.css";

const Logo = () => <span>{'Api Explorer'}</span>;

GraphiQL.Logo = Logo;

function graphQLFetcher(graphQLParams) {
  return fetch(`${window.location.protocol}//${window.location.host}/graphql`, {
    method: "post",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(graphQLParams),
    credentials: "same-origin",
  })
    .then(function (response) {
      return response.text();
    })
    .then(function (responseBody) {
      try {
        return JSON.parse(responseBody);
      } catch (error) {
        return responseBody;
      }
    });
}

ReactDOM.render(
    <GraphiQL style={{ height: '100vh' }} fetcher={graphQLFetcher} />,
   document.getElementById("root")
);
