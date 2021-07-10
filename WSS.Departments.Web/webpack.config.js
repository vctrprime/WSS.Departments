const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const outputPath = path.resolve(__dirname + '/wwwroot', 'dist');

module.exports = {
    entry: './src/js/app.js',
    output: {
        path: outputPath,
        filename: 'bundle.js'
    },
    module: {
        rules: [
            {
                test: /\.(s*)css$/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader',
                    'sass-loader',
                ]
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                },
            },
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            path: outputPath,
            filename: 'build.css',
        }),
        new webpack.ProvidePlugin({
            "React": "react",
        }),
    ]
};