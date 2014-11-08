/*!
 * Mvc.Grid 0.2.0
 * https://github.com/NonFactors/MVC.Grid
 *
 * Copyright © 2014 NonFactors
 *
 * Licensed under the terms of the MIT License
 * http://www.opensource.org/licenses/mit-license.php
 */

$(".mvc-grid-table th[data-sort='True']").on('click', function () {
    window.location.href = $(this).data('sort-query');
});