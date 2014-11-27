/*!
 * Mvc.Grid 0.5.0
 * https://github.com/NonFactors/MVC.Grid
 *
 * Copyright © 2014 NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

(function ($) {
    function MvcGrid(element) {
        this.init($(element).find('table'));
    }

    MvcGrid.prototype = {
        init: function (table) {
            this.initVariables(table);
            this.initFiltering();
            this.initSorting();

            this.removeGridAtributes();
        },
        initVariables: function (table) {
            this.name = table.data('name');
            this.table = table;
            this.columns = [];

            var gridColumns = this.table.find('.mvc-grid-header');
            for (var col = 0; col < gridColumns.length; col++) {
                var gridColumn = $(gridColumns[col]);
                this.columns[col] = {
                    element: gridColumn,
                    name: gridColumn.data('name') || "",
                    filter: {
                        isEnabled: gridColumn.data('filterable') || "",
                        name: gridColumn.data('filter-name') || "",
                        type: gridColumn.data('filter-type') || "",
                        value: gridColumn.data('filter-val') || ""
                    },
                    sort: {
                        isEnabled: gridColumn.data('sortable') || "",
                        order: gridColumn.data('sort-order') || "",
                        query: gridColumn.data('sort-query') || ""
                    }
                };
            }
        },
        initFiltering: function () {
            for (var col = 0; col < this.columns.length; col++) {
                if (this.columns[col].filter.isEnabled == "True") {
                    this.bindFiltering(this.columns[col]);
                }
            }
        },
        bindFiltering: function (column) {
            var that = this;

            column.element.find('.mvc-grid-filter').bind('click.mvcgrid', function (e) {
                e.preventDefault();

                that.renderFilterPopupFor(this, column);
            });
        },
        initSorting: function () {
            for (var col = 0; col < this.columns.length; col++) {
                if (this.columns[col].sort.isEnabled == "True") {
                    this.bindSorting(this.columns[col]);
                }
            }
        },
        bindSorting: function (column) {
            column.element.bind('click.mvcgrid', function (e) {
                var target = $(e.target || e.srcElement);
                if (!target.hasClass("mvc-grid-filter") && target.parents(".mvc-grid-filter").length == 0) {
                    window.location.href = column.sort.query;
                }
            });
        },

        renderFilterPopupFor: function (filter, column) {
            var popup = $('body').children('.mvc-grid-filter-popup');
            var uiFilter = $.fn.mvcgrid.filters[column.filter.name];

            if (uiFilter) {
                popup.find('.popup-content').html(uiFilter.render(column.filter));
                this.setFilterPopupPosition($(filter), popup);
                uiFilter.bindEvents(this, column, popup);
                popup.addClass('open');

                $(window).bind('click.mvcgrid', function (e) {
                    var target = $(e.target || e.srcElement);
                    if (!target.hasClass("mvc-grid-filter") && target.parents(".mvc-grid-filter-popup").length == 0) {
                        $(window).unbind('click.mvcgrid');
                        popup.removeClass("open");
                    }
                });
            } else {
                $(window).unbind('click.mvcgrid');
                popup.removeClass("open");
            }
        },
        setFilterPopupPosition: function (filter, popup) {
            var arrow = popup.find('.popup-arrow');
            var filterLeft = filter.offset().left;
            var filterTop = filter.offset().top;
            var filterHeight = filter.height();
            var winWidth = $(window).width();
            var popupWidth = popup.width();

            var popupTop = filterTop + filterHeight / 2 + 14;
            var popupLeft = filterLeft - 8;
            var arrowLeft = 15;

            if (filterLeft + popupWidth + 5 > winWidth) {
                popupLeft = winWidth - popupWidth - 14;
                arrowLeft = filterLeft - popupLeft + 7;
            }

            arrow.css('left', arrowLeft + 'px');
            popup.css('left', popupLeft + 'px');
            popup.css('top', popupTop + 'px');
        },
        formFilterQueryFor: function (column, type, value) {
            var filterParam = encodeURIComponent(this.name + '-' + column.name + '-' + type);
            var columnParam = encodeURIComponent(this.name + '-' + column.name);
            var parameters = window.location.search.replace('?', '').split('&');
            var filterValue = encodeURIComponent(value);
            var paramExists = false;
            var newParameters = [];
            var newParams = 0;

            for (var i = 0; i < parameters.length; i++) {
                if (parameters[i] !== '') {
                    var paramKey = parameters[i].split('=')[0];
                    if (paramKey.indexOf(columnParam) == 0) {
                        parameters[i] = filterParam + '=' + filterValue;
                        paramExists = true;
                    }

                    newParameters[newParams++] = parameters[i];
                }
            }
            if (!paramExists) {
                newParameters.push(filterParam + '=' + filterValue);
            }

            return '?' + newParameters.join('&');
        },

        removeGridAtributes: function () {
            this.table.removeAttr('data-name');
            for (var col = 0; col < this.columns.length; col++) {
                this.removeGridColumnAttributes(this.columns[col].element);
            }
        },
        removeGridColumnAttributes: function (column) {
            column.removeAttr('data-name');

            column.removeAttr('data-filterable');
            column.removeAttr('data-filter-name');
            column.removeAttr('data-filter-type');
            column.removeAttr('data-filter-val');

            column.removeAttr('data-sortable');
            column.removeAttr('data-sort-order');
            column.removeAttr('data-sort-query');
        }
    };

    $('body').append('<div class="mvc-grid-filter-popup dropdown-menu">' +
                        '<div class="popup-arrow"></div>' +
                        '<div class="popup-content"></div>' +
                     '</div>');

    $(window).resize(function () {
        $(".mvc-grid-filter-popup").removeClass("open");
    });

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

            GridTextFilter.prototype = {
                render: function (filter) {
                    return (
                        '<div class="form-group">' +
                            '<select class="mvc-grid-filter-type form-control">' +
                                '<option value="Equals"' + (filter.type == 'Equals' ? ' selected="selected"' : '') + '>Equals</option>' +
                                '<option value="Contains"' + (filter.type == 'Contains' ? ' selected="selected"' : '') + '>Contains</option>' +
                            '</select>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<input class="form-control mvc-grid-input" type="text" value="' + filter.value + '">' +
                        '</div>' +
                        '<div class="mvc-grid-filter-buttons row">' +
                            '<div class="mvc-grid-left-button col-sm-8">' +
                                '<button class="btn btn-success btn-block mvc-grid-filter-apply" type="button">&#10004;</button>' +
                            '</div>' +
                            '<div class="mvc-grid-right-button col-sm-4">' +
                                '<button class="btn btn-danger btn-block mvc-grid-filter-apply" type="button">&#10008;</button>' +
                            '</div>' +
                        '</div>');
                },
                bindEvents: function (mvcGrid, column, popup) {
                    var applyButton = popup.find('.mvc-grid-filter-apply');
                    applyButton.bind('click.mvcgrid', function () {
                        var type = popup.find('.mvc-grid-filter-type').val();
                        var value = popup.find('.mvc-grid-input').val();
                        popup.removeClass('open');

                        window.location.href = mvcGrid.formFilterQueryFor(column, type, value);
                    });

                    var filterInput = popup.find('.mvc-grid-input');
                    filterInput.bind('keyup', function (e) {
                        column.filter.value = this.value;
                        if (e.keyCode == 13) {
                            applyButton.click();
                        }
                    });
                }
            }

            return new GridTextFilter();
        })(jQuery),
        Number: (function ($) {
            function GridNumberFilter() {
            }

            GridNumberFilter.prototype = {
                render: function (filter) {
                    return (
                        '<div class="form-group">' +
                            '<select class="mvc-grid-filter-type form-control">' +
                                '<option value="Equals"' + (filter.type == 'Equals' ? ' selected="selected"' : '') + '>Equals</option>' +
                                '<option value="LessThan"' + (filter.type == 'LessThan' ? ' selected="selected"' : '') + '>Less than</option>' +
                                '<option value="GreaterThan"' + (filter.type == 'GreaterThan' ? ' selected="selected"' : '') + '>Greater than</option>' +
                                '<option value="LessThanOrEqual"' + (filter.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>Less than or equal</option>' +
                                '<option value="GreaterThanOrEqual"' + (filter.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>Greater than or equal</option>' +
                            '</select>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<input class="form-control mvc-grid-input" type="text" value="' + filter.value + '">' +
                        '</div>' +
                        '<div class="mvc-grid-filter-buttons row">' +
                            '<div class="mvc-grid-left-button col-sm-8">' +
                                '<button class="btn btn-success btn-block mvc-grid-filter-apply" type="button">&#10004;</button>' +
                            '</div>' +
                            '<div class="mvc-grid-right-button col-sm-4">' +
                                '<button class="btn btn-danger btn-block mvc-grid-filter-apply" type="button">&#10008;</button>' +
                            '</div>' +
                        '</div>');
                },
                bindEvents: function (mvcGrid, column, popup) {
                    var applyButton = popup.find('.mvc-grid-filter-apply');
                    applyButton.bind('click.mvcgrid', function () {
                        var type = popup.find('.mvc-grid-filter-type').val();
                        var value = popup.find('.mvc-grid-input').val();
                        popup.removeClass('open');

                        window.location.href = mvcGrid.formFilterQueryFor(column, type, value);
                    });

                    var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[.,]?\\d*$');
                    var filterInput = popup.find('.mvc-grid-input');
                    filterInput.bind('keyup', function (e) {
                        column.filter.value = this.value;
                        if (pattern.test(this.value)) {
                            $(this).removeClass("invalid");
                            if (e.keyCode == 13) {
                                applyButton.click();
                            }
                        } else {
                            $(this).addClass("invalid");
                        }
                    });
                }
            }

            return new GridNumberFilter();
        })(jQuery)
    };

    $('.mvc-grid').mvcgrid();
})(jQuery);
