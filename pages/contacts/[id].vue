<template>
    <div class="contact-page">

        <Head>
            <Title>{{ contact.firstName }}</Title>
        </Head>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <h1 class="display-4">{{ contact.firstName }} {{ contact.lastName }}</h1>
                    <p>Kategoria: {{ contactCategory.category }}</p>
                    <p v-if="contactCategory.categoryId != 2">Podkategoria: {{ contactSubCategory.subCategory }}</p>
                    <p>Email: {{ contact.email }}</p>
                    <p>Numer telefonu: {{ contact.phoneNumber }}</p>
                    <p>Data urodzenia: {{ contact.dateOfBirth }}</p>

                </div>
                <NuxtLink to="/" class="btn btn-primary">Powrót</NuxtLink>

            </div>
        </div>
    </div>
</template>

<script>

export default {
    data() {
        return {
            contact: [],
            contactCategory: {
                categoryId: 0,
                category: ""
            },
            contactSubCategory: {
                subCategoryId: 0,
                subCategory: ""
            }
        }
    },
    async mounted() {
        const response = await $fetch('https://localhost:7160/api/Contact/' + this.$route.params.id)
        this.contact = response
        // workaround a weird bug
        this.contactCategory = this.contact.contactCategory
        this.contactSubCategory = this.contact.contactSubCategory

    }
}

</script>

<style scoped>
.contact-page {
    margin-top: 40px;
}

.container {
    margin-top: 40px;

}

.display-4 {
    margin-bottom: 40px;
}
</style>