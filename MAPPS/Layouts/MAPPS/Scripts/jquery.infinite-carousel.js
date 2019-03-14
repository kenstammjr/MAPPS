/**
 * @author Stéphane Roucheray 
 * @extends jquery
 * @updated By Sean Jordan to ensure it doesn't lose it's jquery references
 */
 
(function ($) {
    $.fn.carousel = function (previous, next, options) {
        var sliderList = $(this).children()[0];
        console.log('this is interior to the function');
        console.log($.fn.jquery);
        console.log(this);
        if (sliderList) {
            var increment = $(sliderList).children().outerWidth("true"),
            elmnts = $(sliderList).children(),
            numElmts = elmnts.length,
            sizeFirstElmnt = increment,
            shownInViewport = Math.round($(this).width() / sizeFirstElmnt),
            firstElementOnViewPort = 1,
            isAnimating = false;

            for (i = 0; i < shownInViewport; i++) {
                $(sliderList).css('width', (numElmts + shownInViewport) * increment + increment + "px");
                $(sliderList).append($(elmnts[i]).clone());
            }

            $(previous).click(function (event) {
                if (!isAnimating) {
                    if (firstElementOnViewPort == 1) {
                        $(sliderList).css('left', "-" + numElmts * sizeFirstElmnt + "px");
                        firstElementOnViewPort = numElmts;
                    }
                    else {
                        firstElementOnViewPort--;
                    }

                    $(sliderList).animate({
                        left: "+=" + increment,
                        y: 0,
                        queue: true
                    }, "swing", function () { isAnimating = false; });
                    isAnimating = true;
                }

            });

            $(next).click(function (event) {
                if (!isAnimating) {
                    if (firstElementOnViewPort > numElmts) {
                        firstElementOnViewPort = 2;
                        $(sliderList).css('left', "0px");
                    }
                    else {
                        firstElementOnViewPort++;
                    }
                    $(sliderList).animate({
                        left: "-=" + increment,
                        y: 0,
                        queue: true
                    }, "swing", function () { isAnimating = false; });
                    isAnimating = true;
                }
            });
        }
    };
}(jQuery));