'use strict'

const run = require('run-sequence')

exports.fn = (gulp, paths, argv, done) => {
  const browserSync = require('browser-sync').create('server')
  const proxy = require('proxy-middleware')
  const url = require('url')

  let proxyOptions = url.parse('http://localhost:5000/api')
  proxyOptions.route = '/api'

  browserSync.init({
    server: {
      baseDir: paths.src,
      middleware: [proxy(proxyOptions)]
    }
  })

  gulp.watch([paths.src + '**/*.ts', paths.test + '**/*.ts', '!' + paths.jspmPackages], ['lint:ts', 'watch:js', 'unit-tests'])
  gulp.watch([paths.src + '**/*.css', '!' + paths.jspmPackages], ['lint:css', 'watch:css'])
  gulp.watch([paths.src + '**/*.html', '!' + paths.jspmPackages], ['lint:html', 'watch:reload'])
  gulp.watch([paths.src + '**/*.{png,jpg,gif,svg,ico}', '!' + paths.jspmPackages], ['watch:reload'])

  run('watch:js')
}
