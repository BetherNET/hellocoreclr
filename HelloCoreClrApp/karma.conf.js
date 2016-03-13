module.exports = function (config) {
  'use strict'

  config.set({
    frameworks: ['mocha', 'chai'],
    files: [
      {pattern: 'wwwroot/lib/angular/angular.js', instrument: false},
      'wwwroot/app/**/*.js',
      'wwwroot/test/**/*.spec.js'
    ],

    singleRun: true,
    browsers: ['PhantomJS']
  })
}
