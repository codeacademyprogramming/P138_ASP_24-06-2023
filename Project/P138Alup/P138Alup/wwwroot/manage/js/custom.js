$(document).ready(function () {
    let check = $('#IsMain').is(':checked');

    if (check) {
        $('.parent').addClass('d-none');
        $('.file').removeClass('d-none');
    } else {
        $('.parent').removeClass('d-none');
        $('.file').addClass('d-none')
    }

    $('#IsMain').click(function () {
        let check = $(this).is(':checked');

        if (check) {
            $('.parent').addClass('d-none');
            $('.file').removeClass('d-none');
        } else {
            $('.parent').removeClass('d-none');
            $('.file').addClass('d-none')
        }
    })

    $(document).on('click', '.deleteImage', function (e) {
        e.preventDefault();

        let url = $(this).attr('href');
        fetch(url)
            .then(res => res.text())
            .then(data => {
                $('.imageContainer').html(data);
            })
    })

})