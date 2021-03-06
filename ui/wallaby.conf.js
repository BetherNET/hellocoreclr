module.exports = (wallaby) => {
  'use strict'

  return {
    testFramework: 'mocha',

    files: [
      {pattern: 'node_modules/systemjs/dist/system.js', instrument: false},
      {pattern: 'node_modules/core-js/client/core.js', instrument: false},
      {pattern: 'src/jspm.conf.js', instrument: false},
      {pattern: 'src/app/**/*.ts', load: false},
      {pattern: 'test/stubs.ts', load: false}
    ],
    tests: [
      {pattern: 'test/*.spec.ts', load: false},
      {pattern: 'test/**/*.spec.ts', load: false}
    ],

    // telling wallaby to serve jspm_packages project folder
    // as is from wallaby web server
    middleware: (app, express) => {
      app.use('/node_modules/systemjs/dist',
              express.static('node_modules/systemjs/dist'))
      app.use('/jspm_packages',
              express.static('src/jspm_packages'))
    },

    setup: (wallaby) => {
      // Preventing wallaby from starting the test run
      wallaby.delayStart()

      var promises = []
      for (var i = 0, len = wallaby.tests.length; i < len; i++) {
        promises.push(System['import'](wallaby.tests[i]))
      }

      // starting wallaby test run when everything required is loaded
      Promise.all(promises).then(function () {
        wallaby.start()
      }).catch(function (e) {
        setTimeout(function () { throw e }, 0)
      })
    }
  }
}
