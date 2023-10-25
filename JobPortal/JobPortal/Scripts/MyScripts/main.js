function validateFname(input) {
    var fname = input.value.trim();
    var errorSpan = document.getElementById('error-fname');

    if (fname === '') {
        errorSpan.textContent = 'First name is required';
        input.classList.add('is-invalid');
        return false
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
        return true
    }
}

function validateLname(input) {
    var lname = input.value.trim();
    var errorSpan = document.getElementById('error-lname');

    if (lname === '') {
        errorSpan.textContent = 'Last name is required';
        input.classList.add('is-invalid');
        return false
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
        return true
    }
}

function validateExperience() {
    var selectElement = document.querySelector('select[name="Experience"]');
    var selectedValue = selectElement.value;
    var errorSpan = document.getElementById('error-experience');

    if (selectedValue === "Experience") {
        errorSpan.textContent = 'Please select your experience';
        selectElement.classList.add('is-invalid');
        return false
    } else {
        errorSpan.textContent = '';
        selectElement.classList.remove('is-invalid');
        return true
    }
}

function disableFutureDates(dateElement) {
    // Get today's date
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;
    dateElement.max = today;
}
function disablePastDates(dateElement) {
    // Get today's date
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;
    dateElement.min= today;
}
function validateDate(input) {
    var selectedDate = new Date(input.value);
    var currentDate = new Date();
    var age = currentDate.getFullYear() - selectedDate.getFullYear();

    if (isNaN(selectedDate)) {
        document.getElementById('error-date').textContent = 'Please select a valid date of birth.';
        document.getElementById('age').textContent = '';
        return false;
    }

    if (age < 18) {
        document.getElementById('error-date').textContent = 'You must be at least 18 years old.';
        document.getElementById('age').textContent = '';
        input.value = '';
        return false;
    } else {
        document.getElementById('error-date').textContent = '';
        document.getElementById('age').textContent = 'Age: ' + age + ' years';
        return true;
    }
}


function validateGender() {
    var genderOptions = document.getElementsByName("Gender");
    var errorSpan = document.getElementById('error-gender');

    for (var i = 0; i < genderOptions.length; i++) {
        if (genderOptions[i].checked) {
            errorSpan.textContent = '';
            return true;
        }
    }

    errorSpan.textContent = 'Please select a gender.';
    return false;
}
function validateDesignation(input) {
    var value = input.value;
    var errorSpan = document.getElementById("error-Designation");

    if (value.trim() === "") {
        errorSpan.textContent = "Designation is required.";
        return false;
    } else {
        errorSpan.textContent = "";
        return true;
    }
}

function validateLogoUpload(input) {
    var errorSpan = document.getElementById("error-logoUpload");
    var maxFileSize = 500 * 1024;

    if (input.files.length === 0) {
        errorSpan.textContent = "Please select a company logo.";
        return false;
    } else {
        var file = input.files[0];
        if (file.size > maxFileSize) {
            errorSpan.textContent = "Logo size should not exceed 500KB.";
            input.value = '';
            return false;
        } else {
            errorSpan.textContent = "";
            return true;
        }
    }
}
    function validatePhone(input) {
        var phoneNumber = input.value;
        var errorSpan = document.getElementById('error-phone');

        if (/^\d{10}$/.test(phoneNumber)) {
            errorSpan.textContent = '';
            return true;
        } else {
            errorSpan.textContent = 'Please enter a valid 10-digit phone number.';
            return false;
        }
    }
    function validateOfficialEmail(input) {
        var email = input.value;
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        var errorSpan = document.getElementById('error-officialemail');

        if (!emailRegex.test(email)) {
            errorSpan.textContent = 'Invalid email address';
            input.classList.add('is-invalid');
            return false;
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
            return true;
        }
    }
    function validateEmail(input) {
        var email = input.value;
        var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        var errorSpan = document.getElementById('error-email');

        if (!emailRegex.test(email)) {
            errorSpan.textContent = 'Invalid email address';
            input.classList.add('is-invalid');
            return false;
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
            return true;
        }
    }
    function validateWebsite(input) {
        var value = input.value;
        var errorSpan = document.getElementById("error-Website");
        if (value.trim() === "") {
            errorSpan.textContent = "Website is required.";
            return false;
        } else if (!isValidURL(value)) {
            errorSpan.textContent = "Please enter a valid URL.";
            return false;
        } else {
            errorSpan.textContent = "";
            return true;
        }
    }

    function validateName(input) {
        var value = input.value;
        var errorSpan = document.getElementById("error-Name");
        if (value.trim() === "") {
            errorSpan.textContent = "Name is required.";
            return false;
        } else if (!/^[A-Za-z\s]+$/.test(value)) {
            errorSpan.textContent = "Name can only contain letters and spaces.";
            return false;
        } else {
            errorSpan.textContent = "";
            return true;
        }
    }
function validateCompanyName(input) {
    var value = input.value;
    var errorSpan = document.getElementById("error-companyname");
    if (value.trim() === "") {
        errorSpan.textContent = "Company name is required.";
        return false;
    } else if (!/^[A-Za-z\s]+$/.test(value)) {
        errorSpan.textContent = "Company name can only contain letters and spaces.";
        return false;
    } else {
        errorSpan.textContent = "";
        return true;
    }
}
    function isValidURL(str) {
        var pattern = /^(http(s)?:\/\/)?(www\.)?[\w\.-]+\.\w+(:\d+)?(\/\S*)?$/;
        return pattern.test(str);
    }
    function validateAddress(input) {
        var address = input.value;
        var errorSpan = document.getElementById('error-address');

        if (address.trim() === '') {
            errorSpan.textContent = 'Address is required';
            input.classList.add('is-invalid');
            return false;
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
            return true;
        }
    }

    const stateData = [
        { name: 'Kerala', cities: ['Trivandrum', 'Kochi', 'Kozhikode'] },
        { name: 'Tamil Nadu', cities: ['Chennai', 'Coimbatore', 'Madurai'] },
        { name: 'Karnataka', cities: ['Bangalore', 'Mysore', 'Hubli'] },
    ];

    const stateSelect = document.getElementById('state');
    const citySelect = document.getElementById('city');
    const errorState = document.getElementById('error-state');
    const errorCity = document.getElementById('error-city');
    stateData.forEach((state) => {
        const option = document.createElement('option');
        option.value = state.name;
        option.text = state.name;
        stateSelect.appendChild(option);
    });

    function updateCity() {
        const selectedState = stateSelect.value;
        const selectedStateData = stateData.find((state) => state.name === selectedState);

        citySelect.innerHTML = '<option>Select a city</option>';

        if (selectedStateData) {
            selectedStateData.cities.forEach((city) => {
                const option = document.createElement('option');
                option.value = city;
                option.text = city;
                citySelect.appendChild(option);
            });
        }
    }

    function validateStateCity() {
        const selectedState = stateSelect.value;
        const selectedCity = citySelect.value;

        if (selectedState === 'Select a state') {
            errorCity.textContent = 'Please select a state.';
            return false;
        } else {
            errorCity.textContent = '';
        }
        if (selectedCity === 'Select a city') {
            errorState.textContent = 'Please select a city.';
            return false;
        } else {
            errorState.textContent = '';
        }
        return true;
    }

    function validateUsername(username) {
        if (username === '') {
            $("#error-username").text("Username is required").removeClass("success-msg").addClass("error-msg");
            return false;
        }
        else if (username.length >= 4) {
            $.ajax({
                url: "CheckUsername/?username=" + username,
                type: "GET",
                success: function (data, textStatus, xhr) {
                    if (xhr.status === 200) {
                        $("#error-username").text("Username is available").removeClass("error-msg").addClass("success-msg");
                        console.log("Available");
                    } else if (xhr.status === 202) {
                        $("#error-username").text("Username is alredy taken").removeClass("success-msg").addClass("error-msg");
                        console.log("Not Available");
                        return false;
                    }
                }
            });
        } else {
            $("#error-username").text("Username should contain atleast 4 charcters").removeClass("success-msg").addClass("error-msg");
            return false;
        }
        return true;
    }
    function validateUsernameOld(input) {
        var username = input.value.trim();
        var errorSpan = document.getElementById('error-username');

        if (username === '') {
            errorSpan.textContent = 'Username is required';
            input.classList.add('is-invalid');
            return false;
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
            return true;
        }
    }

    function validatePass(input) {
        var password = input.value.trim();
        var errorSpan = document.getElementById('error-password');
        var hasUpperCase = /[A-Z]/.test(password);
        var hasLowerCase = /[a-z]/.test(password);
        var hasDigits = /\d/.test(password);
        var hasSpecialChars = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(password);
        var isLengthValid = password.length >= 8;

        if (password === '') {
            errorSpan.textContent = 'Password is required';
            input.classList.add('is-invalid');
            return false;
        } else if (!isLengthValid) {
            errorSpan.textContent = 'Password should be at least 8 characters long';
            input.classList.add('is-invalid');
            return false;
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
        }

        if (!hasUpperCase) {
            errorSpan.textContent += '\nMust include at least one uppercase letter';
            input.classList.add('is-invalid');
            return false;
        }
        if (!hasLowerCase) {
            errorSpan.textContent += '\nMust include at least one lowercase letter';
            input.classList.add('is-invalid');
            return false;
        }
        if (!hasDigits) {
            errorSpan.textContent += '\nMust include at least one digit';
            input.classList.add('is-invalid');
            return false;
        }
        if (!hasSpecialChars) {
            errorSpan.textContent += '\nMust include at least one special character';
            input.classList.add('is-invalid');
            return false;
        }

        return true;
    }

    function validateCpass(input) {
        var password = document.getElementById('Password').value.trim();
        var confirmPassword = input.value.trim();
        var errorSpan = document.getElementById('error-cpassword');

        if (confirmPassword === '') {
            errorSpan.textContent = 'Confirm Password is required';
            input.classList.add('is-invalid');
            return false;
        } else if (password !== confirmPassword) {
            errorSpan.textContent = 'Passwords do not match';
            input.classList.add('is-invalid');
            return false;
        } else {
            errorSpan.textContent = '';
            input.classList.remove('is-invalid');
            return true;
        }
    }
    function validateImage(input) {
        const allowedExtensions = ['.png', '.jpg'];
        const maxSizeInBytes = 500 * 1024;
        const file = input.files[0];
        const errorSpan = document.getElementById('error-image');

        if (!file) {
            errorSpan.textContent = 'Please select a profile picture';
            return false;
        }
        const fileName = file.name;
        const fileExtension = fileName.split('.').pop().toLowerCase();

        if (!allowedExtensions.includes('.' + fileExtension)) {
            errorSpan.textContent = 'Invalid file format. Allowed formats: .png, .jpg';
            input.value = '';
            return false;
        } else {
            errorSpan.textContent = '';
        }

        if (file.size > maxSizeInBytes) {
            errorSpan.textContent = 'File size exceeds 500 KB';
            input.value = '';
            return false;
        }

        return true;
    }

    function validateResume(input) {
        const allowedExtensions = ['.pdf'];
        const maxSizeInBytes = 1024 * 1024;

        const file = input.files[0];
        const errorSpan = document.getElementById('error-resume');

        if (!file) {
            errorSpan.textContent = 'Please select a resume file';
            return false;
        }
        const fileName = file.name;
        const fileExtension = fileName.split('.').pop().toLowerCase();

        if (!allowedExtensions.includes('.' + fileExtension)) {
            errorSpan.textContent = 'Invalid file format. Allowed format: .pdf';
            input.value = '';
            return false;
        } else {
            errorSpan.textContent = '';
        }

        if (file.size > maxSizeInBytes) {
            errorSpan.textContent = 'File size exceeds 1 MB';
            input.value = '';
            return false;
        }

        return true;
    }

    function validateRegisterForm() {
        // Call all the individual validation functions and store their results
        const isImageValid = validateImage(document.getElementById('imageUpload'));
        const isResumeValid = validateResume(document.getElementById('resumeUpload'));
        const isUsernameValid = validateUsername(document.getElementById('username').value);
        const isPasswordValid = validatePass(document.getElementById('Password'));
        const isCPasswordValid = validateCpass(document.getElementById('ConfirmPassword'));
        const isExperienceValid = validateExperience();
        const isDateValid = validateDate(document.getElementById('DateOfBirth'));
        const isGenderValid = validateGender();
        const isPhoneValid = validatePhone(document.getElementById('PhoneNumber'));
        const isEmailValid = validateEmail(document.getElementById('Email'));
        const isAddressValid = validateAddress(document.getElementById('Address'));
        const isFnameValid = validateFname(document.getElementById('FirstName'));
        const isLnameValid = validateLname(document.getElementById('LastName'));
        const isStateCityValid = validateStateCity();
        if (
            isImageValid &&
            isResumeValid &&
            isUsernameValid &&
            isPasswordValid &&
            isCPasswordValid &&
            isExperienceValid &&
            isDateValid &&
            isGenderValid &&
            isPhoneValid &&
            isEmailValid &&
            isAddressValid &&
            isStateCityValid &&
            isFnameValid &&
            isLnameValid
        ) {
            return true;
        } else {
            return false;
        }
}
function validateUpdateForm() {
    const isExperienceValid = validateExperience();
    const isDateValid = validateDate(document.getElementById('DateOfBirth'));
    const isGenderValid = validateGender();
    const isPhoneValid = validatePhone(document.getElementById('PhoneNumber'));
    const isEmailValid = validateEmail(document.getElementById('Email'));
    const isAddressValid = validateAddress(document.getElementById('Address'));
    const isFnameValid = validateFname(document.getElementById('FirstName'));
    const isLnameValid = validateLname(document.getElementById('LastName'));
    const isStateCityValid = validateStateCity();
    if (
        isExperienceValid &&
        isDateValid &&
        isGenderValid &&
        isPhoneValid &&
        isEmailValid &&
        isAddressValid &&
        isStateCityValid &&
        isFnameValid &&
        isLnameValid
    ) {
        return true;
    } else {
        return false;
    }
}
function validateEmployerRegisterForm() {
    const isCompanyLogo = validateLogoUpload(document.getElementById('logoUpload'));
    const isUsernameValid = validateUsername(document.getElementById('username').value);
    const isPasswordValid = validatePass(document.getElementById('Password'));
    const isCPasswordValid = validateCpass(document.getElementById('ConfirmPassword'));
    const isPhoneValid = validatePhone(document.getElementById('ContactPhone'));
    const isEmailValid = validateEmail(document.getElementById('Email'));
    const isOfficialEmailValid = validateOfficialEmail(document.getElementById('officialemail'));
    const isNameValid = validateName(document.getElementById('Name'));
    const isCompanyNameValid = validateCompanyName(document.getElementById('companyname'));
    const isWebsite = validateWebsite(document.getElementById('Website'))
    if (
        isCompanyLogo &&
        isUsernameValid &&
        isPasswordValid &&
        isCPasswordValid &&
        isOfficialEmailValid &&
        isNameValid &&
        isCompanyNameValid &&
        isPhoneValid &&
        isEmailValid &&
        isWebsite
    ) {
        return true;
    } else {
        return false;
    }
}
function validateEmployerUpdateForm() {
    const isPhoneValid = validatePhone(document.getElementById('ContactPhone'));
    const isEmailValid = validateEmail(document.getElementById('Email'));
    const isOfficialEmailValid = validateOfficialEmail(document.getElementById('officialemail'));
    const isNameValid = validateName(document.getElementById('Name'));
    const isCompanyNameValid = validateCompanyName(document.getElementById('companyname'));
    const isWebsite = validateWebsite(document.getElementById('Website'))
    if (
        isOfficialEmailValid &&
        isNameValid &&
        isCompanyNameValid &&
        isPhoneValid &&
        isEmailValid &&
        isWebsite
    ) {
        return true;
    } else {
        return false;
    }
}

function validateOldPass(input) {
    var oldPassword = input.value.trim();
    var errorSpan = document.getElementById('error-oldpassword');

    if (oldPassword === '') {
        errorSpan.textContent = 'Password is required';
        input.classList.add('is-invalid');
        return false;
    } else {
        errorSpan.textContent = '';
        input.classList.remove('is-invalid');
        return true;
    }
    return true;
}
function validateChangePassword() {
    const isOldPasswordValid= validateOldPass(document.getElementById('oldPassword'));
    const isPasswordValid = validatePass(document.getElementById('Password'));
    const isCPasswordValid = validateCpass(document.getElementById('ConfirmPassword'));
    if (
        isPasswordValid &&
        isCPasswordValid &&
        isOldPasswordValid
    ) {
        return true;
    } else {
        return false;
    }
}
function validateAdminForm() {
    const isUsernameValid = validateUsername(document.getElementById('username').value);
    const isPasswordValid = validatePass(document.getElementById('Password'));
    const isCPasswordValid = validateCpass(document.getElementById('ConfirmPassword'));
    const isPhoneValid = validatePhone(document.getElementById('ContactPhone'));
    const isEmailValid = validateEmail(document.getElementById('Email'));
    const isNameValid = validateName(document.getElementById('Name'));
    if (
        isUsernameValid &&
        isPasswordValid &&
        isCPasswordValid &&
        isNameValid &&
        isPhoneValid &&
        isEmailValid 
    ) {
        return true;
    } else {
        return false;
    }
}
function validateUniversity(input) {
    const errorSpan = document.getElementById("error-universityList");
    errorSpan.textContent = ""; 

    if (input.selectedIndex === 0 ) {
        errorSpan.textContent = "University is required.";
        return false;
    }
    return true;
}

function validateMajor(input) {
    const errorSpan = document.getElementById("error-major");
    errorSpan.textContent = "";

    if (input.value === "") {
        errorSpan.textContent = "Major is required.";
        return false;
    }
    return true;
}

function validateDegree(input) {
    const errorSpan = document.getElementById("error-degree");
    errorSpan.textContent = "";

    if (input.value === "") {
        errorSpan.textContent = "Degree is required.";
        return false;
    }
    return true;
}
function validateGPA(input) {
    if (input === "") {
        errorSpan.textContent = "GPA is required";
        return false;
    }
    const errorSpan = document.getElementById("error-gpa");
    errorSpan.textContent = "";

    const gpa = parseFloat(input.value);

    if (isNaN(gpa) || gpa < 0 || gpa > 10) {
        errorSpan.textContent = "GPA should be between 0 and 10";
        return false;
    }
    return true;
}

function populateYearDropdown() {
    const graduationYearSelect = document.getElementById("graduationYear");
    const currentYear = new Date().getFullYear();

    for (let year = 1900; year <= currentYear; year++) {
        const option = document.createElement("option");
        option.value = year;
        option.textContent = year;
        graduationYearSelect.appendChild(option);
    }
    graduationYearSelect.value = new Date().getFullYear();
}

function validateGraduationYear(input) {
    const errorSpan = document.getElementById("error-graduationYear");
    errorSpan.textContent = "";
    if (input.value === "0") {
        errorSpan.textContent = "Please select a Graduation Year.";
        return false;
    }
    return true;
}
function ValidateEducationForm() {
    const iSGraduavtionYear = validateGraduationYear(document.getElementById('graduationYear'));
    const isUniversity = validateUniversity(document.getElementById("universityList"))
    const isGpa = validateGPA(document.getElementById("gpa"))
    const isDegree = validateDegree(document.getElementById("degree"))
    const isMajor = validateMajor(document.getElementById("major"))
    if (isDegree &&
        isGpa &&
        isMajor &&
        isUniversity &&
        iSGraduavtionYear
    ) {
        return true;
    }
    else {
        return false;
    }
}

function validateJobTitle() {
    const jobTitleInput = document.getElementById("JobTitle");
    const errorSpan = document.getElementById("error-JobTitle");
    errorSpan.textContent = "";

    if (jobTitleInput.value === "") {
        errorSpan.textContent = "Job Title is required.";
        return false;
    }
    return true;
}

function validateDescription() {
    const descriptionInput = document.getElementById("Description");
    const errorSpan = document.getElementById("error-Description");
    errorSpan.textContent = "";
    const wordCount = descriptionInput.value.trim().split(/\s+/).filter(Boolean).length;

    if (wordCount < 100) {
        errorSpan.textContent = "Description must contain at least 100 words.";
        return false;
    }
    return true;
}

function validateCategory() {
    const categorySelect = document.getElementById("CategoryID");
    const errorSpan = document.getElementById("error-CategoryID");
    errorSpan.textContent = "";

    if (categorySelect.selectedIndex === 0) {
        errorSpan.textContent = "Category is required.";
        return false;
    }
    return true;
}

function validateLocation() {
    const locationInput = document.getElementById("Location");
    const errorSpan = document.getElementById("error-Location");
    errorSpan.textContent = "";

    if (locationInput.value === "") {
        errorSpan.textContent = "Location is required.";
        return false;
    }
    return true;
}

function validateSalary() {
    const salaryInput = document.getElementById("Salary");
    const errorSpan = document.getElementById("error-Salary");
    errorSpan.textContent = "";

    if (salaryInput.value === "") {
        errorSpan.textContent = "Salary is required.";
        return false;
    }
    return true;
}

function validateEmploymentType() {
    const employmentTypeInput = document.getElementById("EmploymentType");
    const errorSpan = document.getElementById("error-EmploymentType");
    errorSpan.textContent = "";

    if (employmentTypeInput.value === "") {
        errorSpan.textContent = "Employment Type is required.";
        return false;
    }
    return true;
}

function validateApplicationDeadline() {
    const applicationDeadlineInput = document.getElementById("ApplicationDeadline");
    const errorSpan = document.getElementById("error-ApplicationDeadline");
    errorSpan.textContent = "";

    if (applicationDeadlineInput.value === "") {
        errorSpan.textContent = "Application Deadline is required.";
        return false;
    }
    return true;
}
function validateJobForm() {
    const isJobTitleValid = validateJobTitle();
    const isDescriptionValid = validateDescription();
    const isCategoryValid = validateCategory();
    const isLocationValid = validateLocation();
    const isSalaryValid = validateSalary();
    const isEmploymentTypeValid = validateEmploymentType();
    const isApplicationDeadlineValid = validateApplicationDeadline();

    if (
        isJobTitleValid &&
        isDescriptionValid &&
        isCategoryValid &&
        isLocationValid &&
        isSalaryValid &&
        isEmploymentTypeValid &&
        isApplicationDeadlineValid
    ) {
        return true;
    }
    return false;
}
function validateForm() {
    var username = document.getElementById("Username").value;
    var password = document.getElementById("Password").value;
    var errorUsername = document.getElementById("error-username");
    var errorPassword = document.getElementById("error-password");
    errorUsername.innerHTML = "";
    errorPassword.innerHTML = "";
    var isValid = true;

    if (username === "") {
        errorUsername.innerHTML = "Please enter a username.";
        isValid = false;
    }
    if (password === "") {
        errorPassword.innerHTML = "Please enter a password.";
        isValid = false;
    }
    return isValid;
}
