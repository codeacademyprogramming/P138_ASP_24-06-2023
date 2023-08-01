$(document).ready(function () {
    let page = window.location.pathname.split('/')[1];

    if (page.length == 0) {
        $('.Home').addClass('active');
    } else {
        $('.' + page).addClass('active')
    }

    $('.searchBtn').click(function (e) {
        e.preventDefault();

        let search = $('.searchInput').val();
        let categoryId = $('.categoriesList').val();
        
        if (search.length >= 3 ) {
            fetch('product/search/' + categoryId + '?search=' + search)
                .then(res => {
                    return res.text();
                    //return res.json();
                }).then(data => {
                    //let liItem = "";

                    //for (var i = 0; i < data.length; i++) {
                    //    let li = `<li>
                    //                                <a href="#" class="d-flex justify-content-between align-items-center">
                    //                                    <img style="width:100px;" src="/assets/images/product/${data[i].mainImage}"/>
                    //                                    <p>${data[i].title}</p>
                    //                                    <span class="price-sale">€${data[i].price}</span>
                    //                                </a>
                    //                            </li>`;

                    //    liItem += li;
                    //}
                    console.log(data)
                    $('.searchList').html(data);
                    //$('.searchList').html(liItem);
                })
        }
        console.log(search + ' ' + categoryId);
    })

    $('.searchInput').keyup(function () {
        let search = $(this).val();

        if (search.length < 3) {
            $('.searchList').html("");
        }
    })

    $('.modalBtn').click(function (e) {
        e.preventDefault();

        let url = $(this).attr('href');

        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('.modal-content').html(data);
                $('.quick-view-image').slick({
                    slidesToShow: 1,
                    slidesToScroll: 1,
                    arrows: false,
                    dots: false,
                    fade: true,
                    asNavFor: '.quick-view-thumb',
                    speed: 400,
                });

                $('.quick-view-thumb').slick({
                    slidesToShow: 4,
                    slidesToScroll: 1,
                    asNavFor: '.quick-view-image',
                    dots: false,
                    arrows: false,
                    focusOnSelect: true,
                    speed: 400,
                });
            })
    })

    $(document).on('click', '.basketBtn', function (e) {
        e.preventDefault();

        let url = $(this).attr('href');

        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('.header-cart').html(data)
            })
    })
})