<template>
    <div class="edit-page">

        <Head>
            <Title>Edit</Title>
        </Head>
        <h1>Edit</h1>
        <form @submit.prevent="editContact">
            <div class="form-group">
                <label for="firstName">Imię</label>
                <input type="text" v-model="contact.firstName" class="form-control" id="firstName"
                    aria-describedby="firstNameHelp" placeholder="Enter first name">
            </div>
            <div class="form-group">
                <label for="lastName">Nazwisko</label>
                <input type="text" v-model="contact.lastName" class="form-control" id="lastName"
                    aria-describedby="lastNameHelp" placeholder="Enter last name">
            </div>
            <div class="form-group">
                <label for="email">Email</label>
                <input type="email" v-model="contact.email" class="form-control" id="email" aria-describedby="emailHelp"
                    placeholder="Enter email">

            </div>
            <div class="form-group">
                <label for="password">Hasło</label>
                <input type="password" v-model="contact.password" class="form-control" id="password" placeholder="Password">
            </div>
            <div class="form-group">
                <label for="category">Kategoria</label>
                <select class="form-control" v-model="contact.categoryId" id="category">
                    <option value="1">Służbowy</option>
                    <option value="2">Prywatny</option>
                    <option value="3">Inny</option>
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
                <label for="phoneNumber">Numer telefonu</label>
                <input type="text" v-model="contact.phoneNumber" class="form-control" id="phoneNumber"
                    placeholder="Phone Number">
            </div>
            <div class="form-group">
                <label for="dateOfBirth">Data urodzenia</label>
                <input type="date" v-model="contact.dateOfBirth" class="form-control" id="dateOfBirth"
                    placeholder="Date of Birth">
            </div>
            <button type="submit" class="btn btn-primary">Zatwierdź</button>
        </form>
    </div>
</template>

<script>

export default {
    data() {
        return {
            contact: []
        }
    },
    methods: {
        async editContact() {
            // check if logged in
            if (document.cookie != "loggedIn=true") {
                alert("You are not logged in")
                this.$router.push('/')
            }
            this.contact.contactCategory.categoryId = this.contact.categoryId
            this.contact.contactSubCategory.subCategoryId = this.contact.subCategoryId
            // set category and subcategory
            switch (this.contact.categoryId) {
                case '1':

                    switch (this.contact.subCategoryId) {
                        case '1':
                            this.contact.contactSubCategory.subCategory = "Szef"
                            break;
                        case '2':
                            this.contact.contactSubCategory.subCategory = "Klient"
                            break;
                        case '3':
                            this.contact.contactSubCategory.subCategory = "Dostawca"
                            break;
                    }
                    this.contact.contactCategory.category = "Służbowy"
                    break;
                case '2':
                    this.contact.contactCategory.category = "Prywatny"
                    break;
                case '3':
                    this.contact.contactCategory.category = "Inny"
                    break;
            }
            // update contact with new data
            this.contact = {
                contactId: this.contact.contactId,
                firstName: this.contact.firstName,
                lastName: this.contact.lastName,
                email: this.contact.email,
                password: this.contact.password,
                contactCategory: {
                    categoryId: this.contact.categoryId,
                    category: this.contact.contactCategory.category
                },
                categoryId: this.contact.categoryId,
                contactSubCategory: {
                    subCategoryId: this.contact.subCategoryId,
                    subCategory: this.contact.contactSubCategory.subCategory
                },
                subCategoryId: this.contact.subCategoryId,
                phoneNumber: this.contact.phoneNumber,
                dateOfBirth: this.contact.dateOfBirth
            }
            const response = await $fetch('https://localhost:7160/api/Contact/' + this.$route.params.id, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(this.contact)
            })
            this.$router.push('/')
        }

    },
    async mounted() {
        const response = await $fetch('https://localhost:7160/api/Contact/' + this.$route.params.id)
        this.contact = response
    }
}

</script>

<style scoped>
.edit-page {
    margin-top: 40px;
}

.container {
    margin-top: 40px;

}

.display-4 {
    margin-bottom: 40px;
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

.form-group select {
    margin-bottom: 10px;
}
</style>