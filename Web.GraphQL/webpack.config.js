const HtmlWebpackPlugin = require("html-webpack-plugin");
const path = require("path");
const isDev = process.env.NODE_ENV === "development";

module.exports = {
  entry: isDev
    ? [
        "react-hot-loader/patch",
        "webpack-dev-server/client?http://localhost:8080",
        "webpack/hot/only-dev-server",
        "./index.jsx",
      ]
    : "./index.jsx",
  context: path.resolve(__dirname, "./ApiExplorer"),
  output: {
    path: __dirname + "/wwwroot",
    filename: "bundle.js",
  },
  mode: "development",
  devtool: "inline-source-map",
  performance: {
    hints: false,
  },
  module: {
    rules: [
      {
        test: /\.html$/,
        use: ["file?name=[name].[ext]"],
      },
      {
        type: "javascript/auto",
        test: /\.mjs$/,
        use: [],
        include: /node_modules/,
      },
      {
        test: /\.(js|jsx)$/,
        use: [
          {
            loader: "babel-loader",
            options: {
              presets: [
                ["@babel/preset-env", { modules: false }],
                "@babel/preset-react",
              ],
            },
          },
        ],
      },
      {
        test: /\.css$/,
        use: ["style-loader", "css-loader"],
      },
      {
        test: /\.svg$/,
        use: [{ loader: "svg-inline-loader" }],
      },
    ],
  },
  resolve: {
    extensions: [".js", ".json", ".jsx", ".css", ".mjs"],
  },
  plugins: [new HtmlWebpackPlugin({ template: "index.html.ejs" })],
  devServer: {
    hot: true,
    allowedHosts: ["localhost:5000"],
  },
  node: {
    fs: "empty",
    module: "empty",
  },
};
