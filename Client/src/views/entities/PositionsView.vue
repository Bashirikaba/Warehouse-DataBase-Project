<script setup lang="ts">
import { positionsConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Должности</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <EntityTable api-service="Positions" :config="positionsConfig"></EntityTable>
    </template>
  </Card>
</template>
