<script setup lang="ts">
import { staffConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Персонал</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <EntityTable api-service="Staff" :config="staffConfig"></EntityTable>
    </template>
  </Card>
</template>
