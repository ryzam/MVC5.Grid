/*!
 * Mvc.Grid 0.4.0
 * https://github.com/NonFactors/MVC.Grid
 *
 * Copyright © 2014 NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

(function ($) {
    function MvcGrid(element) {
        this.grid = $(element);
        this.init();
    }

    MvcGrid.prototype = {
        init: function () {
            this.initSorting();
            this.initFiltering();
        },

        initSorting: function () {
            this.grid.find('th[data-sort="True"]').on('click.mvcgrid', function () {
                window.location.href = $(this).data('sort-query');
            });
        },

        initFiltering: function () {
            this.createFilterPopup();
            this.bindFilters();
        },
        createFilterPopup: function () {
            if ($('body').children('.mvc-grid-filter-popup').length == 0) {
                $('body').append('<div class="mvc-grid-filter-popup dropdown-menu">' +
                                    '<div class="popup-arrow"></div>' +
                                    '<div class="popup-content"></div>' +
                                 '</div>');
            }
        },
        bindFilters: function () {
            var that = this;

            this.grid.find('th[data-filter="True"] .mvc-grid-filter').on('click.mvcgrid', function (e) {
                e.stopPropagation();
                e.preventDefault();

                var filterName = $(this).parent().data('filter-name');
                var filterValue = $(this).parent().data('filter-value');
                if ($.fn.mvcgrid.filters[filterName]) {
                    var filterPopup = $('body').children('.mvc-grid-filter-popup');
                    filterPopup.find('.popup-content').html($.fn.mvcgrid.filters[filterName].render(filterValue));
                    filterPopup.addClass('open');

                    that.setFilterPopupPosition($(this), filterPopup);
                }
            });
        },
        setFilterPopupPosition: function (filter, popup) {
            var arrow = popup.find('.popup-arrow');

            var dropdownWidth = popup.width();
            var popupLeft = filter.offset().left;
            var popupTop = filter.offset().top;
            var winWidth = $(window).width();
            var dropdownTop = popupTop + 20;
            var dropdownLeft = 0;
            var arrowLeft = 0;

            if (popupLeft + dropdownWidth + 10 > winWidth) {
                dropdownLeft = winWidth - dropdownWidth - 20;
                arrowLeft = popupLeft - dropdownLeft - 3;
            } else {
                dropdownLeft = popupLeft - 30;
                arrowLeft = 17;
            }

            popup.attr('style', 'display: block; left: ' + dropdownLeft + 'px; top: ' + dropdownTop + 'px !important');
            arrow.css('left', arrowLeft + 'px');
        }
    };

    $.fn.mvcgrid = function () {
        return this.each(function () {
            if (!$.data(this, 'mvc-grid')) {
                $.data(this, 'mvc-grid', new MvcGrid(this));
            }
        });
    };

    $.fn.mvcgrid.filters = {
        Text: (function ($) {
            function GridTextFilter() {
            }

            GridTextFilter.prototype.render = function (value) {
                return (
                    '<div class="form-group">' +
                        '<select class="form-control">' +
                            '<option value="Equals" selected="selected">Equals</option>' +
                        '</select>' +
                    '</div>' +
                    '<div class="form-group">' +
                        '<input class="form-control mvc-grid-input" type="text" value="' + value + '">' +
                    '</div>' +
                    '<div class="mvc-grid-filter-buttons">' +
                        '<button class="btn btn-primary btn-block mvc-grid-filter-apply" type="button">Apply</button>' +
                    '</div>');
            }

            return new GridTextFilter();
        })(jQuery)
    };

    $('.mvc-grid').mvcgrid();
})(jQuery);
