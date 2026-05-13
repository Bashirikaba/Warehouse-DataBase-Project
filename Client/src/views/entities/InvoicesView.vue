<script setup lang="ts">
import { invoicesConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Накладные</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <EntityTable api-service="Invoices" :config="invoicesConfig"></EntityTable>
    </template>
  </Card>
</template>
