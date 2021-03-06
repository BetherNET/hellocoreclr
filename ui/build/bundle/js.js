'use strict'

const flatten = require('gulp-flatten')
const filter = require('gulp-filter')
const replace = require('gulp-replace')

exports.dep = ['bundle:app']
exports.fn = (gulp, paths, argv, done) => {
  const filterSettingsJs = filter(['app-bundle-settings*.js'], {restore: true})
  const regex = /"?applicationMode"?:\s*"\w*"/

  return gulp.src([paths.src + 'app-bundle*.js*', '!' + paths.src + 'app-bundle.conf.js'], { base: '.' })
  .pipe(flatten())
  .pipe(filterSettingsJs)
  .pipe(argv.production
    ? replace(regex, '"applicationMode":"Production"')
    : replace(regex, '"applicationMode":"Staging"'))
  .pipe(filterSettingsJs.restore)
  .pipe(gulp.dest(paths.wwwroot))
}
