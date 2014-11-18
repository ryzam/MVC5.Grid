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
            this.grid.find("th[data-sort='True']").on('click.mvcgrid', function () {
                window.location.href = $(this).data('sort-query');
            });
        },

        initFiltering: function () {
            var that = this;
            this.grid.find("th[data-filter='True'] .mvc-grid-filter").on('click.mvcgrid', function (e) {
                e.stopPropagation();
                e.preventDefault();

                var filterName = $(this).parent().data('filter-name');
                if ($.fn.mvcgrid.filters[filterName]) {
                    var filterPopup = $('body').children('.mvc-grid-filter-popup');
                    if (filterPopup.length > 0) {
                        filterPopup.addClass("open");
                        filterPopup.html($.fn.mvcgrid.filters[filterName]("Equals"));
                    } else {
                        $('body').append("<div class='mvc-grid-filter-popup dropdown-menu open'><div class='popup-arrow'></div>" + $.fn.mvcgrid.filters[filterType]("Equals") + "</div>");
                        filterPopup = $('body').children('.mvc-grid-filter-popup');
                    }

                    that.setFilterPopupPosition($(this), filterPopup);
                }
            });
        },
        setFilterPopupPosition: function (filter, popup) {
            var arrow = popup.find(".popup-arrow");

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

            popup.attr("style", "display: block; left: " + dropdownLeft + "px; top: " + dropdownTop + "px !important");
            arrow.css("left", arrowLeft + "px");
        }
    };

    $.fn.mvcgrid = function () {
        return this.each(function () {
            if (!$.data(this, "mvc-grid")) {
                $.data(this, "mvc-grid", new MvcGrid(this));
            }
        });
    };

    $.fn.mvcgrid.filters = {
        Text: function (value) {
            return (
                '<div class="form-group">' +
                    '<select class="form-control">' +
                        '<option value="Equals" ' + (value == "Equals" ? "selected=\"selected\"" : "") + '>Equals</option>' +
                    '</select>' +
                '</div>' +
                '<div class="form-group">' +
                    '<input class="form-control mvc-grid-input" type="text">' +
                '</div>' +
                '<div class="mvc-grid-filter-buttons">' +
                    '<button class="btn btn-primary btn-block" type="button">Apply</button>' +
                '</div>');
        }
    };

    $('.mvc-grid').mvcgrid();
})(jQuery);
