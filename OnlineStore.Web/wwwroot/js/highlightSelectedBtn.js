$(function () {
    let liTags = $("ul.list-group li.list-group-item");
    let urlParts = document.URL.split("/").slice(-2);

    let url = '/';

    for (let a = 0; a < urlParts.length - 1; a++) {
        url += urlParts[a] + "/";
    }

    url += urlParts[urlParts.length - 1];

    for (let a = 0; a < liTags.length; a++) {
        let hrefValue = $(liTags[a].children).attr("href");

        if (hrefValue === url) {
            $(liTags[a]).addClass("account-menu-selected");
        }
    }
})