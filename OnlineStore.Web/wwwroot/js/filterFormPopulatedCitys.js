$(function () {
    let districts = $("#districtSelect");
    let selectedDistrictId = districts.find(":selected").val();

    let doesHaveStartValue = false;

    if (selectedDistrictId !== 'Choose district') {
        doesHaveStartValue = true;
        startFilter(selectedDistrictId);
    }

    districts.on("change", async function () {
        if (doesHaveStartValue) {
            let options = $("#populatedPlacesSelect").find("option");
            var placeholder = options[options.length - 1];

            placeholder.removeAttribute("hidden")
            placeholder.setAttribute("selected", "selected");
        }

        selectedDistrictId = districts.find(":selected").val();
        startFilter(selectedDistrictId);
    });

    async function startFilter(selectedDistrictId) {
        let populatedPlaces = $("#populatedPlacesSelect");

        filterOptionsByDistrict(selectedDistrictId, populatedPlaces);

        enable(populatedPlaces);
    }

    async function filterOptionsByDistrict(districtId, populatedPlaces) {
        let availibleOptionsIds = await getAvailibleOptionsIds(districtId);
        hideOptions(availibleOptionsIds, populatedPlaces)
    }

    async function getAvailibleOptionsIds(districtId) {
        let availibleOptionsIds = undefined;
        await $.ajax({
            url: "/Identity/DeliveryInfo/GetPopulatedPlacesByDistrict",
            data: { "districtId": districtId },
            method: "get",
            dataType: 'json',
            success: function (response) {
                availibleOptionsIds = response;
            }
        });

        return availibleOptionsIds;
    }

    function hideOptions(allowedOptionsIds, populatedPlaces) {
        let options = $(populatedPlaces.children());

        for (let a = 0; a < options.length; a++) {
            let optionToFilter = options[a].value;

            if (allowedOptionsIds.some(aloi => aloi === optionToFilter) === true) {
                options[a].hidden = false;
            }
            else {
                options[a].hidden = true;
            }
        }
    }

    function enable(populatedPlaces) {
        populatedPlaces.removeAttr("disabled")
    }
})