'use strict'

const del = require('del')

exports.fn = (gulp, paths, argv, done) => {
  return del([paths.src + '**/*.{js,js.map}',
    '!' + paths.src + 'jspm.conf.js',
    '!' + paths.src + 'app-bundle*.js',
    paths.test + '**/*.{js,js.map}',
    '!' + paths.test + 'e2e.spec.js',
    '!' + paths.jspmPackages])
}
