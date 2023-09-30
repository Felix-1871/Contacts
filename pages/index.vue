<template>
  
    <div>
      <Head>
        <Title>Contacts</Title>
      </Head>
      <div class="container">
        <div class="row">
          <div class="col-md-12">
            <h1 class="display-4">Contacts</h1>
            <p class="lead">Simple app to manage contacts</p>
            <router-link to="/contacts/add" class="btn btn-primary">Add Contact</router-link>
            <hr />
          <div class="card" v-for="contact in contacts">
            <h4>{{ contact.firstName }}</h4>
            <p>{{ contact.lastName }}</p>
            <p>{{ contact.email }}</p>
            <div class="buttons">
                <NuxtLink :to="`/contacts/${contact.id}`" class="btn btn-primary">Show more</NuxtLink>
              <NuxtLink :to="`/contacts/edit/${contact.id}`" class="btn btn-primary">Edit</NuxtLink>
              <button class="btn btn-danger">Delete</button>
            </div>
          </div>
                
           </div>
          </div>
        </div>
      </div>
  
  </template>
  
  <script>
  export default{
    data() {
      return {
        contacts: []
      }
    },
    async mounted() {
      const response = await $fetch('https://localhost:7160/api/contact')
      console.log(response) // log the response object
      this.contacts = response
      console.log(this.contacts)
    }
  }
  </script>
  