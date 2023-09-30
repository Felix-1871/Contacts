<template>
    <div>
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
            </div>
            <div class="form-group">
                <label for="password">Hasło</label>
                <input type="password" v-model="contact.password" class="form-control" id="password"
                    placeholder="Enter Password">
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
            }
        }

    },
    methods: {
        async addContact() {
            console.log(this.contact)
            // apppend contact.CategoryId to contact.ContactCategory.CategoryId
            this.contact.ContactCategory.CategoryId = this.contact.CategoryId
            // apppend contact.SubCategoryId to contact.ContactSubCategory.SubCategoryId
            this.contact.ContactSubCategory.SubCategoryId = this.contact.SubCategoryId
            switch (this.contact.CategoryId) {
                case '1':
                    this.contact.ContactCategory.Category = 'Służbowy'
                    switch (this.contact.SubCategoryId) {
                        case '1':
                            this.contact.ContactSubCategory.SubCategory = 'Szef'
                            break;
                        case '2':
                            this.contact.ContactSubCategory.SubCategory = 'Klient'
                            break;
                        case '3':
                            this.contact.ContactSubCategory.SubCategory = 'Dostawca'
                            break;
                    }
                    break;
                case '2':
                    this.contact.ContactCategory.Category = 'Prywatny'
                    break;
                case '3':
                    this.contact.ContactCategory.Category = 'Inne'
                    // fetch for the biggest id in the database, then add 1 to it
                    const response = await fetch('https://localhost:7160/api/Contact/GetBiggestId')
                    const json = await response.json()
                    // extract the id from the json object
                    console.log(json)
                    // add 1 to the id
                    this.contact.SubCategoryId = Number(json) + 1
                    break;
            }
            const response = await fetch('https://localhost:7160/api/Contact', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },

                body: JSON.stringify(this.contact)
            })
            const json = await response.json()
            console.log(json)
            // if response is ok, redirect to the contact page
            if (response.ok) {
                this.$router.push('/contacts')
            }
            else {
                // alert the error message
                alert(json.message)

            }
        }
    }
}

</script>