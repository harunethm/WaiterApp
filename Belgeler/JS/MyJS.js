$(document).ready(function () {

});

function generateListItemForMenu(orderid, name, amount, paidamount, comment, price) {
    var item = '<li class="list-group-item">' +
        '<div class="row">' +
        ' <div class="col-5 productName">' +
        name +
        '  </div>' +
        '<div class="col-2 productAmount">' +
        amount +
        '</div>' +
        '<div class="col-2 productPaidAmount">' +
        amount - paidAmount +
        '</div>' +
        '<div class="col-2 productPrice">' +
        price +
        '</div>' +
        '<div class="col-1">' +
        ' <i class="btn-link fas fa-chevron-right araMenuyeEkle"' +
        'data-orderid="' + orderid + '"' +
        'data-productname="' + name + '"' +
        'data-amount="' + amount + '"' +
        'data-paidamount="' + paidamount + '"' +
        'data-price="' + price + '"' +
        'data-comment="' + comment + '">' +
        '</i>' +
        '</div>' +
        '</div>' +
        '</li>';
    return item;
}

function generateListItemForAraMenu(orderid, name, amount, paidamount, comment, price, onBasket) {
    var item = '<li class="list-group-item">' +
        '<div class="row">' +
        '<div class="col-6">' +
        name +
        '</div >' +
        '<div class="col-4">' +
        price + ' TL' +
        '</div>' +
        '<div class="col-2">' +
        '<i class="btn-link fas ' + ((onBasket == true) ? 'fa-minus sepettenCikar' : 'fa-plus sepeteEkle"') +
        //' onClick="sepeteEkle()" ' +
        'data-orderid="' + orderid + '"' +
        'data-productname="' + name + '"' +
        'data-amount="' + amount + '"' +
        'data-paidamount="' + paidamount + '"' +
        'data-price="' + price + '"' +
        'data-comment="' + comment + '">' +
        '</i>' +
        '</div>' +
        '</div>' +
        '</li>';
    return item;
}

function generateListItemForSepet(orderid, name, amount, paidamount, comment, price) {
    var item = '<li class="list-group-item">' +
        '<div class="row">' +
        '<div class="col-6">' +
        name +
        '</div >' +
        '<div class="col-3">' +
        amount + ' TL' +
        '</div>' +
        '<div class="col-3">' +
        price + ' TL' +
        '</div>' +
        '</div>' +
        '</li>';
    return item;
}

function araMenuyuTemizle() {
    var item = '<li class="list-group-item">' +
        '<div class="row">' +
        ' <div class="col-6 font-weight-bold">Ürün Adı</div>' +
        ' <div class="col-4 font-weight-bold">Birim Fiyat</div>' +
        ' <div class="col-2">#</div>' +
        ' </div>' +
        ' </li>';
    $('#araMenu').html(item);
}




function myajax(url, data, success, error) {
    $.ajax({
        url: url,
        type: 'post',
        dataType: 'json',
        data: "{" + data + "}",
        contentType: 'application/json; charset=utf-8',
        success: success,
        error: error,
    });
}