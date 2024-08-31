var lastGoldPriceInput = document.querySelector('#lastCurrent-gold-price');
var paymentOrderCartDetailsDesktop = document.querySelector('.paymentOrderCart-desktop .content');
var paymentOrderCartDetailsMobile = document.querySelector('.payment-cart .content');
var orderId = document.querySelector('#OrderId').value;
var timerCalculate = $('.timer-calculate');
var payBtnOnline = document.querySelector('#onlinePaymentBtn');
var item = document.createElement('div');


item.classList.add('item');

function addAntiForgeryToken(data) {
    if (!data) {
        data = {};
    }

    var tokenInput = $('input[name=__RequestVerificationToken]');
    if (tokenInput.length) {
        data.__RequestVerificationToken = tokenInput.val();
    }

    return data;
}

if (paymentOrderCartDetailsDesktop) {
    var newItem = `<div class="fistTitle"> فی طلا هنگام خرید :</div><div class="fistTitle"><span class="price">${lastGoldPriceInput.value}</span> تومان</div>`;
    item.innerHTML = newItem;
    paymentOrderCartDetailsDesktop.prepend(item);
}

if (paymentOrderCartDetailsMobile) {
    item.classList.add('d-flex');
    item.classList.add('flex-y-center');
    item.classList.add('flex-x-spaceBetween');
    var newItem = `<div class="firstTitle"> فی طلا هنگام خرید :</div><div class="firstTitle">${lastGoldPriceInput.value}<div class="m-r-4">تومان</div></div>`;
    item.innerHTML = newItem;
    paymentOrderCartDetailsMobile.prepend(item)
}

$(document).ready(function () {

    if (paymentOrderCartDetailsDesktop) {
        $('#goldRecalculateContainer').appendTo('.paymentOrderCart-desktop .footer');
    } else {
        $('#goldRecalculateContainer').appendTo('.payment-cart');
    }

    $('#recalculateGoldPriceBtn').click(function () { });
    $.ajax({
        url: `/GoldOrder/GetTheCountDownStarterPoint?orderId=${orderId}`,
        type: 'GET',
        data: addAntiForgeryToken({ orderId: orderId }),
        success: function (result) {
            console.log(result);
            if (result.Success) {
                $('#recalculateGoldPriceBtn').hide();  // Hide the countdown start button
                timerCalculate.show(); // Make sure the countdown display is visible
                $('#onlinePaymentBtn').removeClass('disabled-btn');
                initializeCountdown(result.Data); // Start the countdown
            } else {
                $('#recalculateGoldPriceBtn').show(); // Show the button if countdown needs to start
                $('#onlinePaymentBtn').addClass("disabled-btn"); // Optionally disable another button
            }
        },
        error: function (xhr, status, error) {
            console.error("Error occurred: " + error);
            $('#recalculateGoldPriceBtn').show(); // Show the button in case of an error
        }
    });
});

// Hide the countdown div initially
$('.timer-calculate').hide();

function initializeCountdown(seconds) {
    var timerSpan = $('.km-order-status');
    timerSpan.text('');
    var countDownDate = new Date().getTime() + seconds * 1000;
    var countdownFunction = setInterval(function () {
        var now = new Date().getTime();
        var distance = countDownDate - now;

        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);
        // Format minutes and seconds to always have two digits

        var formattedMinutes = minutes.toString().padStart(2, '0');
        var formattedSeconds = seconds.toString().padStart(2, '0');

        var countDown = `${formattedMinutes}:${formattedSeconds}`;
        timerSpan.text(countDown);

        if (distance < 0) {
            clearInterval(countdownFunction);
            timerCalculate.hide();
            $('#onlinePaymentBtn').addClass("disabled-btn");
            //timerSpan.text(""); // Handle the end of the countdown
            $('#recalculateGoldPriceBtn').show(); // Optionally show the button again
        }
    }, 1000);
}

// Event handler for the button
$('#recalculateGoldPriceBtn').click(function () {
    $.ajax({
        url: `/GoldOrder/RecalculateGoldPrice/orderId?${orderId}`,
        type: 'POST',
        data: addAntiForgeryToken({ orderId: orderId }),
        success: function (result) {
            console.log(result);
            if (result.Success) {
                $('#recalculateGoldPriceBtn').hide();
                timerCalculate.show();
                $('#onlinePaymentBtn').removeClass('disabled-btn');
                // Start the countdown (e.g., 15 minutes)
                initializeCountdown(result.Data);
            }
        },
        error: function (xhr, status, error) {
            // Handle errors here
        }
    });
});
