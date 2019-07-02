'use strict';

const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');


module.exports = {
    entry: {
        'admin': './ClientApp/admin/main.js',
        //'patient': './ClientApp/patient/main.js',
        //'caregiver': './ClientApp/caregiver/main.js'
    },
    output: {
        filename: '[name].js',
        chunkFilename: '[name]-bundle.js',
        path: path.resolve(__dirname, 'resources'),
        publicPath: '/'
    },

    mode: process.env.NODE_ENV, //'development', //'production',

    module: {
        rules: [
            {
                test: /\.(html)$/,
                use: ['file-loader?name=[name]-[hash].[ext]', 'extract-loader', 'html-loader']
                //use: {
                //    loader: 'html-loader',
                //    options: {
                //        attrs: [':data-src']
                //    }
                //}
            },
            {
                test: /\.css$/,// loader: 'style-loader!css-loader'
                use: [
                    //{ loader: 'style-loader' },
                    { loader: MiniCssExtractPlugin.loader, options: { module: true } },
                    { loader: 'css-loader' },
                    //{ loader: 'sass-loader' }
                ]
            },
            {
                test: /\.(png|svg|jpg|gif)$/,
                use: ['file-loader']
            },
            {
                test: /\.(woff|woff2|eot|ttf|otf)$/,
                loader: 'url-loader?limit=30000&name=[name]-[hash].[ext]',
                options: {
                    publicPath: './'
                }
            }
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: '[name].css',
            chunkFilename: '[name]-bundle.css'
        }),
        new webpack.ProvidePlugin({
            //$: 'jQuery',
            //jQuery: 'jQuery',
            ////'window.jQuery': 'jQuery',
            //'Popper': 'popper.js/dist/umd/popper'

            'jQuery': 'jquery',
            'window.jQuery': 'jquery',
            'jquery': 'jquery',
            'window.jquery': 'jquery',
            '$': 'jquery',
            'window.$': 'jquery'
        })
    ],

    optimization: {
        splitChunks: {
            name: 'vendor',
            chunks: 'all'
        }
    }
};