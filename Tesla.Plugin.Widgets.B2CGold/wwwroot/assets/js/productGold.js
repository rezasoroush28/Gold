//variables

var buttonsAccordion = document.querySelectorAll('.controller-accordion');
var buutonsIcon = document.querySelectorAll('.controller-arrow');
var contentAccordion = document.querySelectorAll('.body-accordion');

var itemAccordionButtons = document.querySelectorAll('.controller-accordion-item');
var itemAccordionContents = document.querySelectorAll('.body-accordion-item');
var itemAccordionIcon = document.querySelectorAll('.controller-item-arrow');

var contentAccordionIndex;
var productId = document.querySelector('input[name="product_id"]').value;
var productForm = document.querySelector('#product-details-form');
var belongingPrice = document.querySelector('.belonging-price');
var commissionTax = document.querySelector('.commissions-tax');
var profit = document.querySelector('.profit');
var productGoldPrice = document.querySelectorAll('.Product-Gold-Price');
var totalWeight = document.querySelectorAll('.total-weight');
var currentGoldPrice = document.querySelector('.current-price-gold-online');
var radioButtons = document.querySelectorAll('.selector__item');
var onlineGoldPriceDesktop = document.querySelector('.online-price-gold-desktop');
var btnAddToCart = document.querySelector('.km-add-product-to-cart');
var PreOrderValuePrice = document.querySelector('.preorder-price-value');
var ManueFacturerProfit = document.querySelector('.manu-facturer-profit');
var commisionTexPrecentage = document.querySelector('.commissions-tax-precentage');
var profitPrecentage = document.querySelector('.profit-precentage');
var manuFacturerProfitPrecentage = document.querySelector('.manu-facturer-profit-precentage');
var totalPrice = document.querySelector('.total-price');

var mobileOnlinePrice = document.querySelector('.price-online-gold-mobile');
var mobilePreorderPay = document.querySelector('.mobile-Preorder-Pay');
var mobileProductWeight = document.querySelector('.mobile-Product-Weight');
var mobileProductPrice = document.querySelector('.mobile-product-price');
var timeMainPreperation = document.querySelector('.time-main-preperation');
var mobileTimeMainPreperation = document.querySelector('.mobile-time-main-preperation');
var mainPrice = document.querySelector('.km-value.km-base-product-price1');
var mobilePreorderPriceMain = document.querySelector('.preorder-price-mobile-main');
var weightSummary = document.querySelector('.weight-summary');
var profitSummary = document.querySelector('.profit-summary');

//end variables

//functions

function SeparationThreeDigits(number) {
    formattedNumber = number.toLocaleString('en-US');
    return formattedNumber;
}

function setupAccordion(buttons, contents, icons) {
    buttons.forEach((button, index) => {
        button.addEventListener('click', () => {
            var content = contents[index];
            var icon = icons[index];

            icon.classList.toggle('active');
            button.classList.toggle('active');

            if (button.classList.contains('active')) {
                // Open the accordion and set maxHeight to the scrollHeight
                content.style.maxHeight = content.scrollHeight + "px";
                content.style.marginBottom = 16 + "px";
            } else {
                // Close the accordion smoothly
                closeAccordion(content);
            }

            // Listen to transition end to update the parent accordion
            content.addEventListener('transitionend', () => {
                updateParentAccordions(content);
            }, { once: true });
        });
    });
}

function closeAccordion(content) {
    // Ensure that the transition is smooth
    requestAnimationFrame(() => {
        // Set maxHeight immediately before closing to ensure the transition is smooth
        content.style.maxHeight = content.scrollHeight + "px";
        requestAnimationFrame(() => {
            content.style.maxHeight = "0";
            content.style.marginBottom = "0";
        });
    });
}

function updateParentAccordions(childContent) {
    let currentContent = childContent.parentElement;
    while (currentContent) {
        if (currentContent.classList.contains('body-accordion')) {
            // Recalculate the maxHeight of the parent accordion
            currentContent.style.maxHeight = "none";
            requestAnimationFrame(() => {
                currentContent.style.maxHeight = currentContent.scrollHeight + "px";
            });
        }
        currentContent = currentContent.parentElement;
    }
}

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

function handleAttributeProduct() {
    fetch(`/B2CShoppingCartGold/productdetails_attributechange?productId=${productId}&validateAttributeConditions=true&loadPicture=True`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded',
        },
        body: $(productForm).serialize()
    }).then(response => {
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        return response.json();
    })
        .then(data => {
            console.log(data)
            var profitMain = Number(data.ProfitPrecentage) + Number(data.ManuFacturerProfitPrecentage);
            if (belongingPrice) {

                FillBelongingPrice(data.BelongingPrice)
            }

            if (commissionTax) {

                FillTaxPrice(data.Tax)
            }

            if (productGoldPrice) {

                FillGoldProductPrice(data.GoldPrice)
            }

            if (totalWeight) {
                FillTotalWeight(data.TotalWeight)
            }

            if (currentGoldPrice) {

                FillCurrentGoldPrice(data.GoldCurrentPrice);
            }

            if (profit) {

                FillVendorProfit(data.VendorProfit);
            }

            if (onlineGoldPriceDesktop) {
                FillOnlineGoldDesktop(data.GoldCurrentPrice);
            }
            if (PreOrderValuePrice) {
                FillPreOrderPrice(data.PreOrderPrice);
            }

            if (ManueFacturerProfit) {
                FillManueFacturerProfit(data.ManufacturerProfit);
            }

            if (manuFacturerProfitPrecentage) {
                FillManuFacturerProfitPrecentage(data.ManuFacturerProfitPrecentage);
            }

            if (profitPrecentage) {
                FillProfitPrecentage(data.ProfitPrecentage);
            }

            if (commisionTexPrecentage) {
                FillCommisionTexPrecentage(data.CommisionTexPrecentage);
            }

            if (totalPrice) {
                FillTotalPrice(data.TotalPrice);
            }

            if (mobileOnlinePrice) {
                FillOnlineMobilePrice(data.GoldCurrentPrice);
            }

            if (mobilePreorderPay) {
                FillMobilePreorderPay(data.PreOrderPrice)
            }

            if (mobileProductWeight) {
                FillMobileProductWeight(data.TotalWeight);
            }

            if (mobileProductPrice) {
                FillMobileProductPrice(data.TotalPrice);
            }

            if (timeMainPreperation) {
                FillTimeMainPreperation(data.PreOrderTime);
            }

            if (mobileTimeMainPreperation) {
                FillMobileTimeMainPreperation(data.PreOrderTime);
            }

            //if (mainPrice) {
            //    FillMainPrice(data.TotalPrice)
            //}

            if (mobilePreorderPriceMain) {
                FilMobilePreorderPriceMain(data.PreOrderPrice)
            }

            if (weightSummary) {
                FillWeightSummaryDesktop(data.TotalWeight);
            }

            if (profitSummary) {
                FillProfitSummaryDesktop(profitMain);
            }
        })
        .catch(error => {
            //console.error('Error occurred:', error);
            //console.error('Error details:', error.message);
        });
}

function FillOnlineMobilePrice(price) {
    mobileOnlinePrice.innerHTML = price.toLocaleString('en-US');
}

function FillTimeMainPreperation(date) {
    timeMainPreperation.innerHTML = date;
}

function FillMobileTimeMainPreperation(date) {
    mobileTimeMainPreperation.innerHTML = date;
}
function FillBelongingPrice(price) {
    if (belongingPrice != null)
        //belongingPrice.innerHTML = SeparationThreeDigits(price);
        belongingPrice.innerHTML = price.toLocaleString('en-US');
}

function FillTaxPrice(taxPrice) {
    if (commissionTax != null)
        //commissionTax.innerHTML = SeparationThreeDigits(taxPrice);
        commissionTax.innerHTML = taxPrice.toLocaleString('en-US');
}

function FillGoldProductPrice(productPrice) {
    //productGoldPrice.innerHTML = SeparationThreeDigits(productPrice);
    productGoldPrice.forEach(function (item) {
        item.innerHTML = productPrice.toLocaleString('en-US');
    })
}
function FillCurrentGoldPrice(price) {
    //currentGoldPrice.innerHTML = SeparationThreeDigits(price);
    currentGoldPrice.innerHTML = price.toLocaleString('en-US');
}
function FillTotalPrice(price) {
    totalPrice.innerHTML = price.toLocaleString('en-US');
}


function FillVendorProfit(price) {
    profit.innerHTML = price.toLocaleString('en-US');
}

function FillMobileProductWeight(weight) {
    mobileProductWeight.innerHTML = weight
}

function FillTotalWeight(weight) {
    totalWeight.forEach(function (item) {
        item.innerHTML = weight;
    })
}

function FillOnlineGoldDesktop(price) {
    onlineGoldPriceDesktop.innerHTML = '';
    onlineGoldPriceDesktop.innerHTML = price.toLocaleString('en-US');
}

function FillPreOrderPrice(price) {
    PreOrderValuePrice.innerHTML = price.toLocaleString('en-US');
}

function FillManueFacturerProfit(price) {
    ManueFacturerProfit.innerHTML = price.toLocaleString('en-US');
}

function FillCommisionTexPrecentage(precentage) {
    commisionTexPrecentage.innerHTML = precentage;
}
function FillProfitPrecentage(precentage) {
    profitPrecentage.innerHTML = precentage;
}

function FillManuFacturerProfitPrecentage(precentage) {
    manuFacturerProfitPrecentage.innerHTML = precentage;
}

function FillMobilePreorderPay(price) {
    mobilePreorderPay.innerHTML = price.toLocaleString('en-US');
}

function FillMobileProductWeight(weight) {
    mobileProductWeight.innerHTML = weight;
}

function FillMobileProductPrice(price) {
    mobileProductPrice.innerHTML = price.toLocaleString('en-US');
}

function FillMainPrice(price) {
    mainPrice.innerHTML = '';
    setTimeout(function () {
        mainPrice.innerHTML = '';
        mainPrice.innerHTML = price.toLocaleString('en-US');
    }, 500)
}

function FilMobilePreorderPriceMain(price) {
    mobilePreorderPriceMain.innerHTML = price.toLocaleString('en-US');
}

function FillWeightSummaryDesktop(data) {
    weightSummary.innerHTML = data;
}

function FillProfitSummaryDesktop(data) {
    profitSummary.innerHTML = data;
}

//end functions

//events

// Initialize main accordions
setupAccordion(buttonsAccordion, contentAccordion, buutonsIcon);

// Initialize nested (item) accordions
setupAccordion(itemAccordionButtons, itemAccordionContents, itemAccordionIcon);

var prepaymentAccordion = document.querySelector('.prepayment');

if (prepaymentAccordion) {
    var prepaymentAccordionButton = prepaymentAccordion.querySelector('.controller-accordion');
    var prepaymentAccordionButtonIcon = prepaymentAccordion.querySelector('.controller-arrow');
    var prepaymentAccordionContent = prepaymentAccordion.querySelector('.body-accordion');
    prepaymentAccordionButton.classList.add('active');
    prepaymentAccordionButton.classList.add('active');
    prepaymentAccordionButtonIcon.classList.add('active');
    prepaymentAccordionContent.style.maxHeight = prepaymentAccordionContent.scrollHeight + "px";
} else {
    prepaymentAccordion = null;
}

radioButtons.forEach(function (item, index) {
    item.addEventListener('click', function (e) {

        setTimeout(function () {
            handleAttributeProduct();
        }, 600)
    })
});
window.addEventListener('load', function () {
    handleAttributeProduct();
});

btnAddToCart.classList.add('btn-add-tocart');
btnAddToCart.classList.add('btn-gold-addtocart');
btnAddToCart.innerHTML = '';
btnAddToCart.innerHTML = ` <img src="/Plugins/Widgets.B2CGold/wwwroot/assets/img/shopping-cart.svg">
                                                        <span>
                                                            افزودن به سبد
                                                        </span>`;

//end events
