'use strict'

const del = require('del')

exports.dep = ['bundle:js']
exports.fn = function (gulp, paths, mode, done) {
  return del(paths.src + './app-bundle*.js')
}
