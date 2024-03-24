let timeout = null;

document.getElementById('liveSearchTitle').addEventListener('keyup', function (e) {
    // Clear existing timeout
    clearTimeout(timeout);

    // Reset the timeout to start again
    timeout = setTimeout(function () {
        LiveSearch()
    }, 800);
});

function LiveSearch() {
    // Get the input value
    let value = document.getElementById('liveSearchTitle').value;
    let menu = document.getElementById("result"); // Update this line
    $.ajax({
        type: "POST",
        url: "/Post/LivePostSearch", // Update the URL to your new action
        data: { search: value },
        datatype: "html",
        success: function (data) {
            // Insert the returned search results html into the result element
            $('#result').html(data);
        }
    });
}