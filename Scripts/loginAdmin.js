function checkLogin() {
    var username = $('#username').val();
    var password = $('#password').val();
    var userType = $('input[name="usertype"]:checked').val();
    $.ajax({
        type: 'POST',
        url: '@Url.Action("Login", "Login")',
        data: {
            username: username,
            password: password,
            userType: userType
        }
}