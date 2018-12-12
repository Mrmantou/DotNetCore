(function ($) {
    $(function () {
        var _$classifyCombobox = $('#ClassifyCombobox');

        _$classifyCombobox.change(function () {
            location.href = 'UrlNav?Classify=' + _$classifyCombobox.val();
        });
    });
})(jQuery);