<template>
    <v-col>
        <v-text-field v-model="editedItem.name"
                      label="Name"></v-text-field>
    </v-col>
</template>

<script>
    export default {
        props: {
            editedItem: Object,
        },
    }
</script>