<template>
    <div class="add-page">
        <h1>Add Contact</h1>
        <form @submit.prevent="addContact">
            <div class="form-group">
                <label for="firstName">Imię</label>
                <input type="text" v-model="contact.firstName" class="form-control" id="firstName"
                    placeholder="Enter First Name">
            </div>
            <div class="form-group">
                <label for="lastName">Nazwisko</label>
                <input type="text" v-model="contact.lastName" class="form-control" id="lastName"
                    placeholder="Enter Last Name">
            </div>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" v-model="contact.email" class="form-control" id="email" placeholder="Enter Email">
                <p class="error" v-if="mailFail">Email musi być unikalny</p>
            </div>
            <div class="form-group">
                <label for="password">Hasło</label>
                <input type="password" v-model="contact.password" class="form-control" id="password"
                    placeholder="Enter Password">
                <p class="error" v-if="passFail">Hasło musi zawierać 8 znaków, 1 małą i dużą literę, 1 cyfrę i znak
                    specjalny</p>
            </div>
            <div class="form-group">
                <label for="phone">Numer telefonu</label>
                <input type="text" v-model="contact.phoneNumber" class="form-control" id="phone" placeholder="Enter Phone">
            </div>
            <div class="form-group">
                <label for="Category">Kategoria</label>
                <select v-model="contact.categoryId" class="form-control" id="Category">
                    <option value="1">Służbowy</option>
                    <option value="2">Prywatny</option>
                    <option value="3">Inne</option>
                </select>
            </div>
            <div class="form-group" v-if="contact.categoryId == 1">
                <label for="SubCategory">Podkategoria</label>
                <select v-model="contact.subCategoryId" class="form-control" id="SubCategory">
                    <option value="1">Szef</option>
                    <option value="2">Klient</option>
                    <option value="3">Dostawca</option>
                </select>
            </div>
            <div class="form-group" v-if="contact.categoryId == 3">
                <label for="SubCategory">Podkategoria</label>
                <input type="text" v-model="contact.contactSubCategory.subCategory" class="form-control" id="SubCategory"
                    placeholder="Enter SubCategory">
            </div>
            <div class="form-group">
                <label for="dateOfBirth">Data urodzenia</label>
                <input type="date" v-model="contact.dateOfBirth" class="form-control" id="dateOfBirth"
                    placeholder="Enter Date of Birth">
            </div>

            <button type="submit" class="btn btn-primary">Submit</button>
        </form>
    </div>
</template>

<script>
// adds a new contact

export default {
    data() {
        return {
            contact: {
                contactId: 0,
                firstName: "",
                lastName: "",
                email: "",
                password: "",
                contactCategory: {
                    categoryId: 0,
                    category: ""
                },
                categoryId: 0,
                contactSubCategory: {
                    subCategoryId: 0,
                    subCategory: ""
                },
                subCategoryId: 0,
                phoneNumber: "",
                dateOfBirth: ""
            },
            passFail: false,
            mailFail: false
        }

    },
    methods: {
        async addContact() {
            // check if user is logged in
            if (document.cookie != "loggedIn=true") {
                alert("You are not logged in")
                this.$router.push('/')
            }
            this.contact.contactCategory.categoryId = this.contact.categoryId
            this.contact.contactSubCategory.subCategoryId = this.contact.subCategoryId
            // set the subcategory name based on the id
            switch (this.contact.categoryId) {
                case '1':

                    switch (this.contact.subCategoryId) {
                        case '1':
                            this.contact.contactSubCategory.subCategory = 'Szef'
                            break;
                        case '2':
                            this.contact.contactSubCategory.subCategory = 'Klient'
                            break;
                        case '3':
                            this.contact.contactSubCategory.subCategory = 'Dostawca'
                            break;
                    }
                    break;
                case '3':

                    this.contact.subCategoryId = 0
                    break;
            }
            const response = await fetch('https://localhost:7160/api/Contact', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },

                body: JSON.stringify(this.contact)
            })

            // if response is ok, redirect to the contact page
            if (response.ok) {
                this.$router.push('/')
            }
            // if response is 400, get the error message and display it
            if (response.status === 400) {

                alert("Wystąpił błąd podczas dodawania kontaktu, sprawdź czy wszystkie pola są wypełnione poprawnie")
                //check if password matches regex pattern
                const regex = new RegExp("^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$")
                if (!regex.test(this.contact.password)) {
                    this.passFail = true
                }
                //check if email is unique
                if (json.message == "Email is already taken.") {
                    this.mailFail = true
                }

            }
        }
    }
}

</script>

<style scoped>
.add-page {
    margin-top: 20px;
}

.form-group {
    margin-bottom: 20px;
}

.form-group label {
    margin-bottom: 10px;
    margin-right: 10px;
}

.form-group input {
    margin-bottom: 10px;
}

.error {
    color: red;
}
</style>