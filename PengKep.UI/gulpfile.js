var gulp = require('gulp'),
sass = require('gulp-sass'),
cleancss = require('gulp-clean-css'),
uglify = require('gulp-uglify'),
concat = require('gulp-concat'),
sourcemaps = require('gulp-sourcemaps'),
livereload = require('gulp-livereload');

var config = {
    sassPath: './Content/scss',
    bowerDir: './bower_components',
    stylePath: './Content',
    scriptPath: './Scripts',
    imagePath: './Images'
};

/* -- sass and fonts --*/
gulp.task('bootstrap-sass', function () {
    return gulp.src(config.sassPath + '/bootstrap-custom.scss')
    .pipe(sass({
        outputStyle: 'compressed',
        includePaths: [
            config.bowerDir + '/bootstrap-sass/assets/stylesheets',
        ]
    }).on('error', sass.logError))
    .pipe(cleancss())
    .pipe(concat('bootstrap.min.css'))
    .pipe(gulp.dest(config.stylePath))
});

gulp.task('bootstrap-fonts', function () {
    return gulp.src(config.bowerDir + '/bootstrap-sass/assets/fonts/bootstrap/**.*')
    .pipe(gulp.dest(config.stylePath + '/fonts'))
});


/* -- styles -- */
gulp.task('angular-bootstrap-nav-tree-style', function () {
    gulp.src(config.bowerDir + '/angular-bootstrap-nav-tree/dist/abn_tree.css')
    .pipe(cleancss())
    .pipe(concat('abn_tree.min.css'))
    .pipe(gulp.dest(config.stylePath));
});

gulp.task('angular-style', function () {
    gulp.src(config.bowerDir + '/angular/angular-csp.css')
    .pipe(cleancss())
    .pipe(concat('angular-csp.min.css'))
    .pipe(gulp.dest(config.stylePath));
});

gulp.task('angular-loading-style', function () {
    gulp.src(config.bowerDir + '/angular-loading/angular-loading.css')
    .pipe(cleancss())
    .pipe(concat('angular-loading.min.css'))
    .pipe(gulp.dest(config.stylePath));
});

gulp.task('animate-style', function () {
    gulp.src(config.bowerDir + '/animate.css/animate.min.css')
    .pipe(gulp.dest(config.stylePath));
});

gulp.task('bootstrap-datetimepicker-style', function () {
    gulp.src(config.bowerDir + '/eonasdan-bootstrap-datepicker/build/css/bootstrap-datetimepicker.min.css')
    .pipe(gulp.dest(config.stylePath));
});

/* -- scripts --*/
gulp.task('angular-bootstrap-nav-tree-script', function () {
    gulp.src(config.bowerDir + '/angular-bootstrap-nav-tree/dist/abn_tree_directive.js')
        .pipe(uglify())
        .pipe(concat('abn_tree_directive.min.js'))
        .pipe(gulp.dest(config.scriptPath));
});

gulp.task('angular-ellipsis', function () {
    gulp.src(config.bowerDir + '/angular-ellipsis/src/angular-ellipsis.min.js')
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('angular-script', function () {
    gulp.src(config.bowerDir + '/angular/angular.min.js')
    .pipe(sourcemaps.init({ loadMaps: true }))
    .pipe(sourcemaps.write('/dev/null', { addComment: false }))
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('angular-loading-script', function () {
    gulp.src(config.bowerDir + '/angular-loading/angular-loading.min.js')
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('angular-elastic-script', function () {
    gulp.src(config.bowerDir + '/angular-elastic/elastic.js')
    .pipe(uglify())
    .pipe(concat('elastic.min.js'))
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('angular-chartjs-script', function () {
    gulp.src(config.bowerDir + '/angular-chart.js/dist/angular-chart.min.js')
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('chartjs-script', function () {
    gulp.src(config.bowerDir + '/chart.js/dist/Chart.min.js')
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('nginfinitescroll-script', function () {
    gulp.src(config.bowerDir + '/ngInfiniteScroll/build/ng-infinite-scroll.min.js')
   .pipe(gulp.dest(config.scriptPath));
});

gulp.task('html5shiv-script', function () {
    gulp.src(config.bowerDir + '/html5shiv/dist/html5shiv.min.js')
   .pipe(gulp.dest(config.scriptPath));
});

gulp.task('jquery-script', function () {
    gulp.src(config.bowerDir + '/jquery/dist/jquery.min.js')
   .pipe(gulp.dest(config.scriptPath));
});

gulp.task('moment-script', function () {
    gulp.src(config.bowerDir + '/moment/min/moment.min.js')
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('respond-script', function () {
    gulp.src(config.bowerDir + '/respond/dest/respond.min.js')
   .pipe(gulp.dest(config.scriptPath));
});

gulp.task('spinner-script', function () {
    gulp.src(config.bowerDir + '/angular-spinner/angular-spinner.min.js')
   .pipe(gulp.dest(config.scriptPath));

    gulp.src(config.bowerDir + '/spin.js/spin.min.js')
    .pipe(gulp.dest(config.scriptPath));
});

gulp.task('reload', function () {
    livereload.reload('./')
});

gulp.task('watch', function () {
    livereload.listen();
    gulp.watch('./Content/*.css', ['reload']);
    gulp.watch('./Scripts/*.js', ['reload']);
    gulp.watch('./Scripts/**/*.js', ['reload']);
    gulp.watch('./Views/**/*.cshtml', ['reload']);
});


gulp.task('sassandfonts', ['bootstrap-sass', 'bootstrap-fonts']);
gulp.task('styles', ['angular-bootstrap-nav-tree-style', 'angular-style', 'angular-loading-style', 'animate-style', 'bootstrap-datetimepicker-style']);
gulp.task('scripts', ['angular-bootstrap-nav-tree-script', 'angular-script', 'angular-chartjs-script', 'angular-ellipsis', 'angular-elastic-script', 'chartjs-script',
    'nginfinitescroll-script', 'html5shiv-script', 'jquery-script', 'angular-loading-script', 'respond-script', 'moment-script', 'spinner-script']);

gulp.task('default', ['sassandfonts','styles', 'scripts'], function () {
});