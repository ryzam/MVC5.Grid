/*!
 * Mvc.Grid 0.8.0
 * https://github.com/NonFactors/MVC.Grid
 *
 * Copyright © 2014 NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

(function ($) {
    function MvcGrid(element, isLoaded, currentQuery) {
        this.init($(element), isLoaded, currentQuery);
    }

    MvcGrid.prototype = {
        init: function (grid, isLoaded, currentQuery) {
            this.initVariables(grid, isLoaded, currentQuery);
            this.initFiltering();
            this.initSorting();
            this.initPaging();

            this.removeGridAtributes();
            this.reloadGrid();
        },
        initVariables: function (grid, isLoaded, currentQuery) {
            this.table = grid.find('.mvc-grid-table');
            this.pager = grid.find('.mvc-grid-pager');
            this.name = grid.data('name');
            this.options = [];
            this.columns = [];
            this.grid = grid;

            this.options.dataSourceUrl = grid.data('source-url') || '';
            this.options.isAjaxGrid = this.options.dataSourceUrl != '';
            this.options.currentQuery = currentQuery || '';
            this.options.isLoaded = isLoaded;

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
            var that = this;
            column.element.bind('click.mvcgrid', function (e) {
                var target = $(e.target || e.srcElement);
                if (!target.hasClass("mvc-grid-filter") && target.parents(".mvc-grid-filter").length == 0) {
                    that.options.isLoaded = false;

                    if (that.options.isAjaxGrid) {
                        that.reloadGrid(column.sort.query);
                    } else {
                        window.location.href = column.sort.query;
                    }
                }
            });
        },
        initPaging: function () {
            var pageLinks = this.pager.find('span');
            for (var page = 0; page < pageLinks.length; page++) {
                this.bindPaging($(pageLinks[page]));
            }

        },
        bindPaging: function (pageLink) {
            var that = this;
            if (pageLink.data('query')) {
                pageLink.bind('click.mvcgrid', function (e) {
                    that.options.isLoaded = false;

                    if (that.options.isAjaxGrid) {
                        that.reloadGrid($(this).data('query'));
                    } else {
                        window.location.href = $(this).data('query');
                    }
                });
            }
        },

        reloadGrid: function (query) {
            var that = this;
            query = (query) ? query : '';

            if (that.options.isAjaxGrid && !that.options.isLoaded) {
                $.ajax({
                    url: that.options.dataSourceUrl + query
                })
                .success(function (result) {
                    that.grid.hide();
                    that.grid.after(result);
                    that.options.isLoaded = true;

                    that.grid.next('.mvc-grid').mvcgrid(true, query);
                    that.grid.remove();
                });
            }
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
        formFilterQueryFor: function (column) {
            var filterParam = encodeURIComponent(this.name + '-' + column.name + '-' + column.filter.type);
            var columnParam = encodeURIComponent(this.name + '-' + column.name);
            var parameters = (this.options.isAjaxGrid ? this.options.currentQuery : window.location.search).replace('?', '').split('&');
            var filterValue = encodeURIComponent(column.filter.value);
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
        formFilterQueryWithout: function (column) {
            var columnParam = encodeURIComponent(this.name + '-' + column.name);
            var parameters = (this.options.isAjaxGrid ? this.options.currentQuery : window.location.search).replace('?', '').split('&');
            var newParameters = [];
            var newParams = 0;

            for (var i = 0; i < parameters.length; i++) {
                if (parameters[i] != '' && parameters[i].indexOf(columnParam) != 0) {
                    newParameters[newParams++] = parameters[i];
                }
            }

            if (newParameters.length == 0)
                return '';

            return '?' + newParameters.join('&');
        },

        removeGridAtributes: function () {
            this.grid.removeAttr('data-name');
            this.grid.removeAttr('data-source-url');
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

    $.fn.mvcgrid = function (isLoaded, currentQuery) {
        return this.each(function () {
            if (!$.data(this, 'mvc-grid')) {
                $.data(this, 'mvc-grid', new MvcGrid(this, isLoaded, currentQuery));
            }
        });
    };
    $.fn.mvcgrid.lang = {
        Text: {
            Contains: "Contains",
            Equals: "Equals",
            StartsWith: "Starts with",
            EndsWith: "EndsWith"
        },
        Number: {
            Equals: "Equals",
            LessThan: "Less than",
            GreaterThan: "Greater than",
            LessThanOrEqual: "Less than or equal",
            GreaterThanOrEqual: "Greater than or equal"
        },
        Date: {
            Equals: "Equals",
            LessThan: "Less than",
            GreaterThan: "Greater than",
            LessThanOrEqual: "Less than or equal",
            GreaterThanOrEqual: "Greater than or equal"
        },
        Boolean: {
            Yes: "Yes",
            No: "No"
        }
    };
    $.fn.mvcgrid.filters = {
        Text: (function ($) {
            function GridTextFilter() {
            }

            GridTextFilter.prototype = {
                render: function (filter) {
                    var lang = $.fn.mvcgrid.lang;

                    return (
                        '<div class="form-group">' +
                            '<select class="mvc-grid-filter-type form-control">' +
                                '<option value="Contains"' + (filter.type == 'Contains' ? ' selected="selected"' : '') + '>' + lang.Text.Contains + '</option>' +
                                '<option value="Equals"' + (filter.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Text.Equals + '</option>' +
                                '<option value="StartsWith"' + (filter.type == 'StartsWith' ? ' selected="selected"' : '') + '>' + lang.Text.StartsWith + '</option>' +
                                '<option value="EndsWith"' + (filter.type == 'EndsWith' ? ' selected="selected"' : '') + '>' + lang.Text.EndsWith + '</option>' +
                            '</select>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<input class="form-control mvc-grid-input" type="text" value="' + filter.value + '">' +
                        '</div>' +
                        '<div class="mvc-grid-filter-buttons row">' +
                            '<div class="mvc-grid-left-button col-sm-6">' +
                                '<button class="btn btn-success btn-block mvc-grid-filter-apply" type="button">&#10004;</button>' +
                            '</div>' +
                            '<div class="mvc-grid-right-button col-sm-6">' +
                                '<button class="btn btn-danger btn-block mvc-grid-filter-cancel" type="button">&#10008;</button>' +
                            '</div>' +
                        '</div>');
                },
                bindEvents: function (mvcGrid, column, popup) {
                    var typeSelect = popup.find('.mvc-grid-filter-type');
                    typeSelect.bind('change.mvcgrid', function () {
                        column.filter.type = this.value;
                    });
                    typeSelect.change();

                    var filterInput = popup.find('.mvc-grid-input');
                    filterInput.bind('keyup.mvcgrid', function (e) {
                        column.filter.value = this.value;
                        if (e.keyCode == 13) {
                            applyButton.click();
                        }
                    });

                    var applyButton = popup.find('.mvc-grid-filter-apply');
                    applyButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryFor(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryFor(column);
                        }
                    });

                    var cancelButton = popup.find('.mvc-grid-filter-cancel');
                    cancelButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryWithout(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryWithout(column);
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
                    var lang = $.fn.mvcgrid.lang;

                    return (
                        '<div class="form-group">' +
                            '<select class="mvc-grid-filter-type form-control">' +
                                '<option value="Equals"' + (filter.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Number.Equals + '</option>' +
                                '<option value="LessThan"' + (filter.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.Number.LessThan + '</option>' +
                                '<option value="GreaterThan"' + (filter.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.Number.GreaterThan + '</option>' +
                                '<option value="LessThanOrEqual"' + (filter.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.Number.LessThanOrEqual + '</option>' +
                                '<option value="GreaterThanOrEqual"' + (filter.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.Number.GreaterThanOrEqual + '</option>' +
                            '</select>' +
                        '</div>' +
                        '<div class="form-group">' +
                            '<input class="form-control mvc-grid-input" type="text" value="' + filter.value + '">' +
                        '</div>' +
                        '<div class="mvc-grid-filter-buttons row">' +
                            '<div class="mvc-grid-left-button col-sm-6">' +
                                '<button class="btn btn-success btn-block mvc-grid-filter-apply" type="button">&#10004;</button>' +
                            '</div>' +
                            '<div class="mvc-grid-right-button col-sm-6">' +
                                '<button class="btn btn-danger btn-block mvc-grid-filter-cancel" type="button">&#10008;</button>' +
                            '</div>' +
                        '</div>');
                },
                bindEvents: function (mvcGrid, column, popup) {
                    var typeSelect = popup.find('.mvc-grid-filter-type');
                    typeSelect.bind('change.mvcgrid', function () {
                        column.filter.type = this.value;
                    });
                    typeSelect.change();

                    var pattern = new RegExp('^(?=.*\\d+.*)[-+]?\\d*[.,]?\\d*$');
                    var filterInput = popup.find('.mvc-grid-input');
                    filterInput.bind('keyup.mvcgrid', function (e) {
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

                    var applyButton = popup.find('.mvc-grid-filter-apply');
                    applyButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryFor(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryFor(column);
                        }
                    });

                    var cancelButton = popup.find('.mvc-grid-filter-cancel');
                    cancelButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryWithout(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryWithout(column);
                        }
                    });
                }
            }

            return new GridNumberFilter();
        })(jQuery),
        Date: (function ($) {
            function GridDateFilter() {
            }

            GridDateFilter.prototype = {
                render: function (filter) {
                    var filterInput = '<input class="form-control mvc-grid-input" type="text" value="' + filter.value + '">';
                    var lang = $.fn.mvcgrid.lang;

                    return (
                        '<div class="form-group">' +
                            '<select class="mvc-grid-filter-type form-control">' +
                                '<option value="Equals"' + (filter.type == 'Equals' ? ' selected="selected"' : '') + '>' + lang.Date.Equals + '</option>' +
                                '<option value="LessThan"' + (filter.type == 'LessThan' ? ' selected="selected"' : '') + '>' + lang.Date.LessThan + '</option>' +
                                '<option value="GreaterThan"' + (filter.type == 'GreaterThan' ? ' selected="selected"' : '') + '>' + lang.Date.GreaterThan + '</option>' +
                                '<option value="LessThanOrEqual"' + (filter.type == 'LessThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.Date.LessThanOrEqual + '</option>' +
                                '<option value="GreaterThanOrEqual"' + (filter.type == 'GreaterThanOrEqual' ? ' selected="selected"' : '') + '>' + lang.Date.GreaterThanOrEqual + '</option>' +
                            '</select>' +
                        '</div>' +
                        '<div class="form-group">' +
                            filterInput +
                        '</div>' +
                        '<div class="mvc-grid-filter-buttons row">' +
                            '<div class="mvc-grid-left-button col-sm-6">' +
                                '<button class="btn btn-success btn-block mvc-grid-filter-apply" type="button">&#10004;</button>' +
                            '</div>' +
                            '<div class="mvc-grid-right-button col-sm-6">' +
                                '<button class="btn btn-danger btn-block mvc-grid-filter-cancel" type="button">&#10008;</button>' +
                            '</div>' +
                        '</div>');
                },
                bindEvents: function (mvcGrid, column, popup) {
                    var typeSelect = popup.find('.mvc-grid-filter-type');
                    typeSelect.bind('change.mvcgrid', function () {
                        column.filter.type = this.value;
                    });
                    typeSelect.change();

                    var filterInput = popup.find('.mvc-grid-input');
                    filterInput.bind('keyup.mvcgrid', function (e) {
                        column.filter.value = this.value;
                        if (e.keyCode == 13) {
                            applyButton.click();
                        }
                    });

                    var applyButton = popup.find('.mvc-grid-filter-apply');
                    applyButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryFor(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryFor(column);
                        }
                    });

                    var cancelButton = popup.find('.mvc-grid-filter-cancel');
                    cancelButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryWithout(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryWithout(column);
                        }
                    });
                }
            }

            return new GridDateFilter();
        })(jQuery),
        Boolean: (function ($) {
            function GridBooleanFilter() {
            }

            GridBooleanFilter.prototype = {
                render: function (filter) {
                    var lang = $.fn.mvcgrid.lang;

                    return (
                        '<div class="form-group">' +
                            '<ul class="mvc-grid-filter-type mvc-grid-boolean-filter">' +
                                '<li ' + (filter.value == 'True' ? 'class="active" ' : '') + 'data-value="True">' + lang.Boolean.Yes + '</li>' +
                                '<li ' + (filter.value == 'False' ? 'class="active" ' : '') + 'data-value="False">' + lang.Boolean.No + '</li>' +
                            '</ul>' +
                        '</div>' +
                        '<div class="mvc-grid-filter-buttons row">' +
                            '<div class="mvc-grid-left-button col-sm-6">' +
                                '<button class="btn btn-success btn-block mvc-grid-filter-apply" type="button">&#10004;</button>' +
                            '</div>' +
                            '<div class="mvc-grid-right-button col-sm-6">' +
                                '<button class="btn btn-danger btn-block mvc-grid-filter-cancel" type="button">&#10008;</button>' +
                            '</div>' +
                        '</div>');
                },
                bindEvents: function (mvcGrid, column, popup) {
                    var valueItems = popup.find('.mvc-grid-filter-type li');
                    column.filter.type = 'Equals';

                    valueItems.bind('click.mvcgrid', function () {
                        var item = $(this);

                        column.filter.value = item.data('value');
                        item.siblings().removeClass("active");
                        item.addClass("active");
                    });

                    var applyButton = popup.find('.mvc-grid-filter-apply');
                    applyButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryFor(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryFor(column);
                        }
                    });

                    var cancelButton = popup.find('.mvc-grid-filter-cancel');
                    cancelButton.bind('click.mvcgrid', function () {
                        mvcGrid.options.isLoaded = false;
                        popup.removeClass('open');

                        if (mvcGrid.options.isAjaxGrid) {
                            mvcGrid.reloadGrid(mvcGrid.formFilterQueryWithout(column));
                        } else {
                            window.location.href = mvcGrid.formFilterQueryWithout(column);
                        }
                    });
                }
            }

            return new GridBooleanFilter();
        })(jQuery)
    };

    $('.mvc-grid').mvcgrid();
})(jQuery);
