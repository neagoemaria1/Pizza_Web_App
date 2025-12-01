function updateNume(event) {
    event.preventDefault();
    var newNume = document.getElementById("nume").value;
    fetch('/Setari/UpdateNume', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'newNume=' + encodeURIComponent(newNume)
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to reflect the changes
                window.location.reload();
            } else {
                console.error('Failed to update last name.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


// Function to update Prenume (first name)
function updatePrenume(event) {
    event.preventDefault();
    var newPrenume = document.getElementById("prenume").value;
    fetch('/Setari/UpdatePrenume', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'newPrenume=' + encodeURIComponent(newPrenume)
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to reflect the changes
                window.location.reload();
            } else {
                console.error('Failed to update first name.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Function to update Email
function updateEmail(event) {
    event.preventDefault();
    var newEmail = document.getElementById("email").value;
    fetch('/Setari/UpdateEmail', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'newEmail=' + encodeURIComponent(newEmail)
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to reflect the changes
                window.location.reload();
            } else {
                console.error('Failed to update email.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

// Function to update Telefon (phone number)
function updateTelefon(event) {
    event.preventDefault();
    var newTelefon = document.getElementById("telefon").value;
    fetch('/Setari/UpdateTelefon', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'newTelefon=' + encodeURIComponent(newTelefon)
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to reflect the changes
                window.location.reload();
            } else {
                console.error('Failed to update phone number.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function ChangePassword(event) {
    event.preventDefault();
    var newPassword = document.getElementById("password").value;
    fetch('/Setari/ChangePassword', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'newPassword=' + encodeURIComponent(newPassword)
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to reflect the changes
                window.location.reload();
            } else {
                console.error('Failed to update password.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


// Function to update Adresa (address)
function updateAdresa(event) {
    event.preventDefault();
    var newAdresa = document.getElementById("adresa").value;
    fetch('/Setari/UpdateAdresa', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/x-www-form-urlencoded'
        },
        body: 'newAdresa=' + encodeURIComponent(newAdresa)
    })
        .then(response => {
            if (response.ok) {
                // Reload the page to reflect the changes
                window.location.reload();
            } else {
                console.error('Failed to update address.');
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
}


var personalInfoSection = document.getElementById("profile");
var settingsSection = document.getElementById("profile-settings");

document.querySelector('.profile-nav a[data-target="profile"]').addEventListener('click', function (event) {
    event.preventDefault();

    personalInfoSection.style.display = "block";
    settingsSection.style.display = "none";
    document.title = 'Pizzeria Toscana/Profile - Personal Info';
});

document.querySelector('.profile-nav a[data-target="profile-settings"]').addEventListener('click', function (event) {
    event.preventDefault();
    settingsSection.style.display = "block";
    personalInfoSection.style.display = "none";
    document.title = 'Pizzeria Toscana/Profile - Settings';
});