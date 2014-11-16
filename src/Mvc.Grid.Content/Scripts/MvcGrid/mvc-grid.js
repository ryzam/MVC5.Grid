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
        },

        initSorting: function () {
            this.grid.find("th[data-sort='True']").on('click.mvcgrid', function () {
                window.location.href = $(this).data('sort-query');
            });
        }
    };

    $.fn.mvcgrid = function () {
        return this.each(function () {
            if (!$.data(this, "mvc-grid")) {
                $.data(this, "mvc-grid", new MvcGrid(this));
            }
        });
    };

    $('.mvc-grid').mvcgrid();
})(jQuery);
